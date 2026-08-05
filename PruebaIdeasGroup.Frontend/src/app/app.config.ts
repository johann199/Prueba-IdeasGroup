import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ESTADO_PROYECTO_REPOSITORY } from './core/interfaces/estado-proyecto-repository.interface';
import { EstadoProyectoApiService } from './core/services/estado-proyecto-api.service';
import { PROYECTO_REPOSITORY } from './core/interfaces/proyecto.interface';
import { ProyectoApiService } from './core/services/proyecto-api.service';
import { COLUMNA_REPOSITORY } from './core/interfaces/columna.interface';
import { ColumnaApiService } from './core/services/columna-api.service';
import { TAREA_REPOSITORY } from './core/interfaces/tarea.interface';
import { TareaApiService } from './core/services/tarea-api.service';
import { USUARIO_REPOSITORY } from './core/interfaces/usuario.interface';
import { UsuarioApiService } from './core/services/usuario-api.service';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(withFetch()),
    provideAnimationsAsync(),
    { provide: ESTADO_PROYECTO_REPOSITORY, useClass: EstadoProyectoApiService },
    { provide: USUARIO_REPOSITORY, useClass: UsuarioApiService },
    { provide: PROYECTO_REPOSITORY, useClass: ProyectoApiService },
    { provide: COLUMNA_REPOSITORY, useClass: ColumnaApiService },
    { provide: TAREA_REPOSITORY, useClass: TareaApiService },
  ]
};
