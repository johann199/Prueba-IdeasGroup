import {InjectionToken} from "@angular/core";
import {Observable} from "rxjs";
import {Usuario, CreateUsuario, UpdateUsuario} from "../models/usuario.model";

export interface IUsuarioRepository{
    getAll(): Observable<Usuario[]>;
    getById(id: number): Observable<Usuario>;
    create(dto: CreateUsuario): Observable<Usuario>;
    update(id: number, dto: UpdateUsuario): Observable<void>;
    delete(id: number): Observable<void>;
}

export const USUARIO_REPOSITORY = new InjectionToken<IUsuarioRepository>('USUARIO_REPOSITORY');