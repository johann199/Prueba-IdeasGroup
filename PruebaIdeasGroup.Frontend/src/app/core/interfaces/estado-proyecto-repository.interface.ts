import {InjectionToken} from "@angular/core";
import {Observable} from "rxjs";
import {EstadoProyecto, CreateEstadoProyecto, UpdateEstadoProyecto} from "../models/estado-proyecto.model";
export interface IEstadoProyectoRepository {
    getAll(): Observable<EstadoProyecto[]>;
    getById(id: number): Observable<EstadoProyecto>;
    create(dto: CreateEstadoProyecto): Observable<EstadoProyecto>;
    update(id: number, dto: UpdateEstadoProyecto): Observable<void>;
    delete(id: number): Observable<void>;
}

export const ESTADO_PROYECTO_REPOSITORY = new InjectionToken<IEstadoProyectoRepository>('ESTADO_PROYECTO_REPOSITORY');