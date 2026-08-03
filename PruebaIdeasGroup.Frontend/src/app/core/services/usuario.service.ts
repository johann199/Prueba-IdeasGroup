import {Inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import { USUARIO_REPOSITORY, IUsuarioRepository } from '../interfaces/usuario.interface';
import {Usuario, CreateUsuario, UpdateUsuario} from '../models/usuario.model';

@Injectable({providedIn: 'root'})
export class UsuarioService{
    constructor(
        @Inject(USUARIO_REPOSITORY) private readonly repository: IUsuarioRepository){}
    
        getAll(): Observable<Usuario[]>{
            return this.repository.getAll();
        }

        create(nombre:string, correo:string, contrasena:string): Observable<Usuario>{
            const nombreTrimmed = nombre.trim();
            if (!nombreTrimmed){
                throw new Error('El nombre de usuario no puede ser vacio')    
            }
            const correoTrimmed = correo.trim();
            if(!correoTrimmed){
                throw new Error('El correo de usuario no puede ser vacio')    
            }
            const contrasenaTrimmed = contrasena.trim();
            if(!contrasenaTrimmed){
                throw new Error('La contraseña del usuario no puede ser vacio')    
            }
            const dto: CreateUsuario = {nombre:nombreTrimmed, correo: correoTrimmed, contrasena:contrasenaTrimmed};
            return this.repository.create(dto);
        }

        update(id: number, nombre: string, correo:string): Observable<void> {
            const dto: UpdateUsuario = { nombre: nombre.trim(), correo:correo.trim() };
            return this.repository.update(id, dto);
        }
        
        delete(id: number): Observable<void> {
            return this.repository.delete(id);
        }
}