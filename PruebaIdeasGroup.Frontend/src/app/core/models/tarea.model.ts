export interface Tarea {
    id: number;
    nombre: string;
    descripcion: string;
    ordenDentroColumna: number;
    prioridad: number;
    columnaId: number;
    responsablesIds: number[];
    creado?: string;
    modificado?: string;
}

export interface CreateTarea {
    nombre: string;
    descripcion: string;
    prioridad: number;
    ordenDentroColumna: number;
    columnaId: number;
}

export interface UpdateTarea {
    nombre: string;
    descripcion: string;
    prioridad: number;
    ordenDentroColumna: number;
    columnaId: number;
}

export interface AddResponsableTarea {
    usuarioId: number;
}