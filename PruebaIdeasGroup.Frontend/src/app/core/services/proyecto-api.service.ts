import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IProyectoRepository } from '../interfaces/proyecto.interface';
import { Proyecto, CreateProyecto, UpdateProyecto } from '../models/proyecto.model';

@Injectable({ providedIn: 'root' })
export class ProyectoApiService implements IProyectoRepository {
    private readonly baseUrl = `${environment.apiUrl}/Proyecto`;

    constructor(private readonly http: HttpClient) {}

    getAll(): Observable<Proyecto[]> {
        return this.http.get<Proyecto[]>(this.baseUrl);
    }

    getById(id: number): Observable<Proyecto> {
        return this.http.get<Proyecto>(`${this.baseUrl}/${id}`);
    }

    create(dto: CreateProyecto): Observable<Proyecto> {
        return this.http.post<Proyecto>(this.baseUrl, dto);
    }

    update(id: number, dto: UpdateProyecto): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}