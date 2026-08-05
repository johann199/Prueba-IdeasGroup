import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';
import { Proyecto, CreateProyecto, UpdateProyecto } from '../models/proyecto.model';

export interface IProyectoRepository {
    getAll(): Observable<Proyecto[]>;
    getById(id: number): Observable<Proyecto>;
    create(dto: CreateProyecto): Observable<Proyecto>;
    update(id: number, dto: UpdateProyecto): Observable<void>;
    delete(id: number): Observable<void>;
}

export const PROYECTO_REPOSITORY = new InjectionToken<IProyectoRepository>('PROYECTO_REPOSITORY');