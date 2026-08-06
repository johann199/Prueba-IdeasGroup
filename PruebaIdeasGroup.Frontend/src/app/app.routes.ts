import { Routes } from '@angular/router';
import { AppLayout } from './layout/component/app.layout';

import { EstadoProyectoListComponent } from './features/estados-proyecto/estado-proyecto-list/estado-proyecto-list.component';
import { UsuarioListComponent } from './features/usuario/usuario-list/usuario-list.component';
import {ProyectoListComponent} from './features/proyecto/proyecto-list/proyecto-list.component';
import { TableroKanbanComponent } from './features/tablero/tablero-kanban/tablero-kanban.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'auth/login',
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: '',
    component: AppLayout,
    canActivate: [authGuard],
    children: [
      {
        path: 'tablero',
        loadComponent: () => import('./features/tablero/tablero-kanban/tablero-kanban.component').then(m => m.TableroKanbanComponent)
      },
      {
        path: 'proyectos',
        loadComponent: () => import('./features/proyecto/proyecto-list/proyecto-list.component').then(m => m.ProyectoListComponent)
      },
      {
        path: 'usuarios',
        loadComponent: () => import('./features/usuario/usuario-list/usuario-list.component').then(m => m.UsuarioListComponent)
      },
      {
        path: 'estados-proyecto',
        loadComponent: () => import('./features/estados-proyecto/estado-proyecto-list/estado-proyecto-list.component').then(m => m.EstadoProyectoListComponent)
      },
      { path: '', redirectTo: 'tablero', pathMatch: 'full' }
    ]
  },
  { path: '**', redirectTo: 'auth/login' }
];