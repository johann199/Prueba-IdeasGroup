import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PROYECTO_REPOSITORY, IProyectoRepository } from '../interfaces/proyecto.interface';
import { Proyecto, CreateProyecto, UpdateProyecto } from '../models/proyecto.model';

@Injectable({ providedIn: 'root' })
export class ProyectoService {
    constructor(@Inject(PROYECTO_REPOSITORY) private readonly repository: IProyectoRepository) {}

    getAll(): Observable<Proyecto[]> {
        return this.repository.getAll();
    }

    create(dto: CreateProyecto): Observable<Proyecto> {
        if (!dto.nombre?.trim()) throw new Error('El nombre del proyecto no puede estar vacío.');
        if (!dto.descripcion?.trim()) throw new Error('La descripción del proyecto no puede estar vacía.');
        return this.repository.create(dto);
    }

    update(id: number, dto: UpdateProyecto): Observable<void> {
        if (!dto.nombre?.trim()) throw new Error('El nombre del proyecto no puede estar vacío.');
        return this.repository.update(id, dto);
    }

    delete(id: number): Observable<void> {
        return this.repository.delete(id);
    }
}