import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TAREA_REPOSITORY, ITareaRepository } from '../interfaces/tarea.interface';
import { Tarea, CreateTarea, UpdateTarea } from '../models/tarea.model';

@Injectable({ providedIn: 'root' })
export class TareaService {
    constructor(@Inject(TAREA_REPOSITORY) private readonly repository: ITareaRepository) {}

    getByColumnaId(columnaId: number): Observable<Tarea[]> {
        return this.repository.getByColumnaId(columnaId);
    }

    create(dto: CreateTarea): Observable<Tarea> {
        if (!dto.nombre?.trim()) throw new Error('El nombre de la tarea no puede estar vacío.');
        return this.repository.create(dto);
    }

    update(id: number, dto: UpdateTarea): Observable<void> {
        return this.repository.update(id, dto);
    }

    addResponsable(tareaId: number, usuarioId: number): Observable<void> {
        return this.repository.addResponsable(tareaId, { usuarioId });
    }

    delete(id: number): Observable<void> {
        return this.repository.delete(id);
    }
}