import { Routes } from '@angular/router';
import {EstadoProyectoListComponent} from './features/estados-proyecto/estado-proyecto-list/estado-proyecto-list.component';
import { UsuarioListComponent } from './features/usuario/usuario-list/usuario-list.component';
import {TableroKanbanComponent} from './features/tablero/tablero-kanban/tablero-kanban.component';
export const routes: Routes = [
  { path: 'estados-proyecto', component: EstadoProyectoListComponent },
  { path: 'usuarios', component: UsuarioListComponent },
  { path: 'tablero', component: TableroKanbanComponent}

];
