import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ITareaRepository } from '../interfaces/tarea.interface';
import { Tarea, CreateTarea, UpdateTarea, AddResponsableTarea } from '../models/tarea.model';

@Injectable({ providedIn: 'root' })
export class TareaApiService implements ITareaRepository {
    private readonly baseUrl = `${environment.apiUrl}/Tarea`;

    constructor(private readonly http: HttpClient) {}

    getById(id: number): Observable<Tarea> {
        return this.http.get<Tarea>(`${this.baseUrl}/${id}`);
    }

    getByColumnaId(columnaId: number): Observable<Tarea[]> {
        return this.http.get<Tarea[]>(`${this.baseUrl}/columna/${columnaId}`);
    }

    create(dto: CreateTarea): Observable<Tarea> {
        return this.http.post<Tarea>(this.baseUrl, dto);
    }

    update(id: number, dto: UpdateTarea): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
    }

    addResponsable(tareaId: number, dto: AddResponsableTarea): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/${tareaId}/responsables`, dto);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}