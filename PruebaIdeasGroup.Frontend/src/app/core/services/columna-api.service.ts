import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IColumnaRepository } from '../interfaces/columna.interface';
import { Columna, CreateColumna, UpdateColumna } from '../models/columna.model';

@Injectable({ providedIn: 'root' })
export class ColumnaApiService implements IColumnaRepository {
    private readonly baseUrl = `${environment.apiUrl}/Columna`;

    constructor(private readonly http: HttpClient) {}

    getById(id: number): Observable<Columna> {
        return this.http.get<Columna>(`${this.baseUrl}/${id}`);
    }

    getByProyectoId(proyectoId: number): Observable<Columna[]> {
        return this.http.get<Columna[]>(`${this.baseUrl}/proyecto/${proyectoId}`);
    }

    create(dto: CreateColumna): Observable<Columna> {
        return this.http.post<Columna>(this.baseUrl, dto);
    }

    update(id: number, dto: UpdateColumna): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}