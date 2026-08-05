export interface Columna {
    id: number;
    nombre: string;
    ordenDentroProyecto: number;
    proyectoId: number;
    creado?: string;
    modificado?: string;
}

export interface CreateColumna {
    nombre: string;
    ordenDentroProyecto: number;
    proyectoId: number;
}

export interface UpdateColumna {
    nombre: string;
    ordenDentroProyecto: number;
}