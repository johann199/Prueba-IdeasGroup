export interface Usuario {
    id : number;
    nombre : string;
    correo : string;
    contrasena : string;
    creado : string;
    modificado : string;
}

export interface CreateUsuario {
    nombre : string;
    correo : string;
    contrasena : string;
}

export interface UpdateUsuario {
    nombre : string;
    correo : string;
}