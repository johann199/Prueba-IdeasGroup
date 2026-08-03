import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import { IUsuarioRepository} from "../interfaces/usuario.interface";
import { Usuario, CreateUsuario, UpdateUsuario } from "../models/usuario.model";


@Injectable({providedIn: 'root'})
export class UsuarioApiService implements IUsuarioRepository {
    private readonly baseUrl = `${environment.apiUrl}/Usuario`;

    constructor(private readonly http: HttpClient){}

    getAll(): Observable<Usuario[]> {
        return this.http.get<Usuario[]>(this.baseUrl);
    }

    getById(id: number): Observable<Usuario> {
        return this.http.get<Usuario>(`${this.baseUrl}/${id}`);
    }

    create(dto: CreateUsuario): Observable<Usuario> {
        return this.http.post<Usuario>(this.baseUrl, dto);
    }

    update(id: number, dto:UpdateUsuario): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
    }

    delete(id: number): Observable<void>{
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}