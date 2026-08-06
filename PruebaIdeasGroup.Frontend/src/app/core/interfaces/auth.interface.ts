export interface LoginRequest {
  correo: string;
  contrasena: string;
}

export interface AuthResponse {
  token: string;
  usuario: {
    id: number;
    nombre: string;
    correo: string;
  };
}