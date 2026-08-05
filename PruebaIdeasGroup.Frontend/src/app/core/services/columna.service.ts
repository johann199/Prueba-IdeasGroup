import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { COLUMNA_REPOSITORY, IColumnaRepository } from '../interfaces/columna.interface';
import { Columna, CreateColumna, UpdateColumna } from '../models/columna.model';

@Injectable({ providedIn: 'root' })
export class ColumnaService {
    constructor(@Inject(COLUMNA_REPOSITORY) private readonly repository: IColumnaRepository) {}

    getByProyectoId(proyectoId: number): Observable<Columna[]> {
        return this.repository.getByProyectoId(proyectoId);
    }

    create(nombre: string, ordenDentroProyecto: number, proyectoId: number): Observable<Columna> {
        const nombreTrimmed = nombre.trim();
        if (!nombreTrimmed) throw new Error('El nombre de la columna no puede estar vacío.');
        
        const dto: CreateColumna = { nombre: nombreTrimmed, ordenDentroProyecto, proyectoId };
        return this.repository.create(dto);
    }

    update(id: number, nombre: string, ordenDentroProyecto: number): Observable<void> {
        const dto: UpdateColumna = { nombre: nombre.trim(), ordenDentroProyecto };
        return this.repository.update(id, dto);
    }

    delete(id: number): Observable<void> {
        return this.repository.delete(id);
    }
}