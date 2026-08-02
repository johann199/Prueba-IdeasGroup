import {Inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import { ESTADO_PROYECTO_REPOSITORY, IEstadoProyectoRepository } from '../interfaces/estado-proyecto-repository.interface'; 
import { EstadoProyecto, CreateEstadoProyecto, UpdateEstadoProyecto } from '../models/estado-proyecto.model';

@Injectable({providedIn: 'root'})
export class EstadoProyectoService {
    constructor(
        @Inject(ESTADO_PROYECTO_REPOSITORY) private readonly repository: IEstadoProyectoRepository) {}

    getAll(): Observable<EstadoProyecto[]> {
        return this.repository.getAll();
    }

    create(nombre: string): Observable<EstadoProyecto> {
        const trimmed = nombre.trim();
        if (!trimmed) {
            throw new Error('El nombre del estado del proyecto no puede estar vacío.');
        }
        const dto: CreateEstadoProyecto = { nombre: trimmed };
        return this.repository.create(dto);
    }

    update(id: number, nombre: string): Observable<void> {
        const dto: UpdateEstadoProyecto = { nombre: nombre.trim() };
        return this.repository.update(id, dto);
    }

    delete(id: number): Observable<void> {
        return this.repository.delete(id);
    }
}