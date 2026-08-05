import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';
import { Columna, CreateColumna, UpdateColumna } from '../models/columna.model';

export interface IColumnaRepository {
    getById(id: number): Observable<Columna>;
    getByProyectoId(proyectoId: number): Observable<Columna[]>;
    create(dto: CreateColumna): Observable<Columna>;
    update(id: number, dto: UpdateColumna): Observable<void>;
    delete(id: number): Observable<void>;
}

export const COLUMNA_REPOSITORY = new InjectionToken<IColumnaRepository>('COLUMNA_REPOSITORY');