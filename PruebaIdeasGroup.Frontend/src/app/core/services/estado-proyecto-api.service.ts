import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IEstadoProyectoRepository} from "../interfaces/estado-proyecto-repository.interface";
import {EstadoProyecto, CreateEstadoProyecto, UpdateEstadoProyecto} from "../models/estado-proyecto.model";

@Injectable({providedIn: 'root'})
export class EstadoProyectoApiService implements IEstadoProyectoRepository {
    private readonly baseUrl = `${environment.apiUrl}/EstadoProyecto`;
    
    constructor(private readonly http: HttpClient) {}

    getAll(): Observable<EstadoProyecto[]> {
        return this.http.get<EstadoProyecto[]>(this.baseUrl);
    }

    getById(id: number): Observable<EstadoProyecto> {
        return this.http.get<EstadoProyecto>(`${this.baseUrl}/${id}`);
    }

    create(dto: CreateEstadoProyecto): Observable<EstadoProyecto> {
        return this.http.post<EstadoProyecto>(this.baseUrl, dto);
    }

    update(id: number, dto: UpdateEstadoProyecto): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}