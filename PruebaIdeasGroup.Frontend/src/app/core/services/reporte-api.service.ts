import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReporteApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl; // Ajusta según la variable de tu environment

  descargarPdfProyecto(proyectoId: number): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/PdfReporte/proyectos/${proyectoId}/pdf`, {
      responseType: 'blob'
    });
  }

  descargarExcelProyecto(proyectoId: number): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/ExcelReporte/proyectos/${proyectoId}/excel`, {
      responseType: 'blob'
    });
  }
}