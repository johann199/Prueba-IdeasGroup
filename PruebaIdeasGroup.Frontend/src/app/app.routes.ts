import { Routes } from '@angular/router';
import { AppLayout } from './layout/component/app.layout';

import { EstadoProyectoListComponent } from './features/estados-proyecto/estado-proyecto-list/estado-proyecto-list.component';
import { UsuarioListComponent } from './features/usuario/usuario-list/usuario-list.component';
import {ProyectoListComponent} from './features/proyecto/proyecto-list/proyecto-list.component';
import { TableroKanbanComponent } from './features/tablero/tablero-kanban/tablero-kanban.component';


export const routes: Routes = [
    {
        path: '',
        component: AppLayout,
        children: [
            { path: 'estados-proyecto', component: EstadoProyectoListComponent },
            { path: 'usuarios', component: UsuarioListComponent },
            { path: 'proyecto', component: ProyectoListComponent },
            { path: 'tablero', component: TableroKanbanComponent }
        ]
    },
    { path: '**', redirectTo: '' }
];