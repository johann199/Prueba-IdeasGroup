import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';
import { Tarea, CreateTarea, UpdateTarea, AddResponsableTarea } from '../models/tarea.model';

export interface ITareaRepository {
    getById(id: number): Observable<Tarea>;
    getByColumnaId(columnaId: number): Observable<Tarea[]>;
    create(dto: CreateTarea): Observable<Tarea>;
    update(id: number, dto: UpdateTarea): Observable<void>;
    addResponsable(tareaId: number, dto: AddResponsableTarea): Observable<void>;
    delete(id: number): Observable<void>;
}

export const TAREA_REPOSITORY = new InjectionToken<ITareaRepository>('TAREA_REPOSITORY');