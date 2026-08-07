import { Injectable, inject } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { AuthService } from './auth.service';
import { environment } from '../../../environments/environment';

export interface TaskMovedEvent {
  tareaId: number;
  columnaId: number;
  nuevoOrden: number;
}

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private authService = inject(AuthService);
  private hubConnection?: signalR.HubConnection;

  public taskMoved$ = new Subject<TaskMovedEvent>();
  public boardUpdated$ = new Subject<void>();

  public startConnection(projectId: number): void {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.joinProjectGroup(projectId);
      return;
    }

    const token = this.authService.getToken();
    const hubUrl = `${environment.apiUrl.replace('/api', '')}/hubs/board`;

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl, {
        accessTokenFactory: () => token || ''
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        this.joinProjectGroup(projectId);
      })
      .catch((err) => console.error('Error al conectar con SignalR Hub:', err));

    this.registerListeners();
  }

  private registerListeners(): void {
    if (!this.hubConnection) return;

    this.hubConnection.on('TaskMoved', (data: TaskMovedEvent) => {
      this.taskMoved$.next(data);
    });

    this.hubConnection.on('BoardUpdated', () => {
      this.boardUpdated$.next();
    });
  }

  public joinProjectGroup(projectId: number): void {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('JoinProjectBoard', projectId.toString());
    }
  }

  public leaveProjectGroup(projectId: number): void {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('LeaveProjectBoard', projectId.toString());
    }
  }

  public stopConnection(projectId: number): void {
    if (this.hubConnection) {
      this.leaveProjectGroup(projectId);
      this.hubConnection.stop();
    }
  }
}