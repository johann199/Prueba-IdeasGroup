export interface Proyecto {
    id: number;
    nombre: string;
    descripcion: string;
    fechaInicio: string;
    fechaFin: string;
    creadoPorId: number;
    estadoId: number;
    creado?: string;
    modificado?: string;
}

export interface CreateProyecto {
    nombre: string;
    descripcion: string;
    fechaInicio: string;
    fechaFin: string;
    creadoPorId: number;
    estadoId: number;
}

export interface UpdateProyecto {
    nombre: string;
    descripcion: string;
    fechaInicio: string;
    fechaFin: string;
    estadoId: number;
}