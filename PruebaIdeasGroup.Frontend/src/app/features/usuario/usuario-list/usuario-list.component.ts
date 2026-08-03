import { Component, OnInit, signal, ViewChild } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { DialogModule } from 'primeng/dialog';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { UsuarioService } from '../../../core/services/usuario.service';
import { Usuario } from '../../../core/models/usuario.model';

@Component({
    selector: 'app-usuario-list',
    standalone: true,
    imports: [
        CommonModule,
        TableModule,
        FormsModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        InputTextModule,
        PasswordModule,
        DialogModule,
        InputIconModule,
        IconFieldModule,
        ConfirmDialogModule,
    ],
    template: `
        <p-toolbar styleClass="mb-4">
            <ng-template pTemplate="start">
                <p-button label="Nuevo" icon="pi pi-plus" (onClick)="openNew()"></p-button>
            </ng-template>
        </p-toolbar>

        <p-table
            #dt
            [value]="usuarios()"
            [rows]="10"
            [paginator]="true"
            [globalFilterFields]="['nombre', 'correo']"
            [tableStyle]="{ 'min-width': '50rem' }"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} usuarios"
            [showCurrentPageReport]="true"
            [rowsPerPageOptions]="[10, 20, 30]"
        >
            <ng-template pTemplate="caption">
                <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
                    <h5 class="m-0 text-xl font-semibold">Usuarios</h5>

                    <p-iconField iconPosition="right" class="w-full sm:w-auto">
                        <p-inputIcon styleClass="pi pi-search"></p-inputIcon>
                        <input
                            pInputText
                            type="text"
                            (input)="onGlobalFilter(dt, $event)"
                            placeholder="Buscar por nombre o correo..."
                            class="w-full sm:w-80"
                        />
                    </p-iconField>
                </div>
            </ng-template>

            <ng-template pTemplate="header">
                <tr>
                    <th pSortableColumn="nombre" style="min-width: 14rem">
                        Nombre
                        <p-sortIcon field="nombre"></p-sortIcon>
                    </th>
                    <th pSortableColumn="correo" style="min-width: 16rem">
                        Correo
                        <p-sortIcon field="correo"></p-sortIcon>
                    </th>
                    <th pSortableColumn="creado" style="min-width: 12rem">
                        Creado
                        <p-sortIcon field="creado"></p-sortIcon>
                    </th>
                    <th style="min-width: 8rem"></th>
                </tr>
            </ng-template>

            <ng-template pTemplate="body" let-usuario>
                <tr>
                    <td style="min-width: 14rem">{{ usuario.nombre }}</td>
                    <td style="min-width: 16rem">{{ usuario.correo }}</td>
                    <td style="min-width: 12rem">{{ usuario.creado | date: 'dd/MM/yyyy HH:mm' }}</td>
                    <td>
                        <p-button icon="pi pi-pencil" class="mr-2" [rounded]="true" [outlined]="true" (click)="editUsuario(usuario)"></p-button>
                        <p-button icon="pi pi-trash" severity="danger" [rounded]="true" [outlined]="true" (click)="deleteUsuario(usuario)"></p-button>
                    </td>
                </tr>
            </ng-template>

            <ng-template pTemplate="emptymessage">
                <tr>
                    <td colspan="4" class="text-center py-4">No hay usuarios todavía.</td>
                </tr>
            </ng-template>
        </p-table>

        <p-dialog [(visible)]="usuarioDialog" [style]="{ width: '450px' }" header="Detalle del Usuario" [modal]="true">
            <ng-template pTemplate="content">
                <div class="flex flex-column gap-3 pt-3">
                    <div>
                        <label for="nombre" class="block font-bold mb-2">Nombre</label>
                        <input type="text" pInputText id="nombre" [(ngModel)]="usuario.nombre" required autofocus class="w-full" />
                        <small class="p-error" *ngIf="submitted && !usuario.nombre">El nombre es obligatorio.</small>
                    </div>

                    <div>
                        <label for="correo" class="block font-bold mb-2">Correo</label>
                        <input type="email" pInputText id="correo" [(ngModel)]="usuario.correo" required class="w-full" />
                        <small class="p-error" *ngIf="submitted && !usuario.correo">El correo es obligatorio.</small>
                    </div>

                    <!-- La contraseña solo se pide al crear. Editar nunca la muestra ni la vuelve
                         a enviar — UpdateUsuario, a propósito, no tiene ese campo. -->
                    <div *ngIf="!esEdicion">
                        <label for="contrasena" class="block font-bold mb-2">Contraseña</label>
                        <p-password
                            id="contrasena"
                            [(ngModel)]="contrasena"
                            [toggleMask]="true"
                            [feedback]="false"
                            styleClass="w-full"
                            inputStyleClass="w-full"
                        ></p-password>
                        <small class="p-error" *ngIf="submitted && !contrasena">La contraseña es obligatoria.</small>
                    </div>
                </div>
            </ng-template>

            <ng-template pTemplate="footer">
                <p-button label="Cancelar" icon="pi pi-times" [text]="true" (click)="hideDialog()"></p-button>
                <p-button label="Guardar" icon="pi pi-check" (click)="guardarUsuario()"></p-button>
            </ng-template>
        </p-dialog>

        <p-confirmDialog [style]="{ width: '450px' }"></p-confirmDialog>
    `,
    providers: [MessageService, ConfirmationService],
})
export class UsuarioListComponent implements OnInit {
    usuarioDialog: boolean = false;

    usuarios = signal<Usuario[]>([]);

    usuario: Partial<Usuario> = {};

    contrasena: string = '';

    esEdicion: boolean = false;

    submitted: boolean = false;

    @ViewChild('dt') dt!: Table;

    constructor(
        private usuarioService: UsuarioService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {}

    ngOnInit() {
        this.cargarUsuarios();
    }

    cargarUsuarios() {
        this.usuarioService.getAll().subscribe({
            next: (data) => this.usuarios.set(data),
            error: () =>
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'No se pudieron cargar los usuarios.',
                }),
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.usuario = {};
        this.contrasena = '';
        this.esEdicion = false;
        this.submitted = false;
        this.usuarioDialog = true;
    }

    editUsuario(usuario: Usuario) {
        this.usuario = { ...usuario };
        this.contrasena = '';
        this.esEdicion = true;
        this.submitted = false;
        this.usuarioDialog = true;
    }

    hideDialog() {
        this.usuarioDialog = false;
        this.submitted = false;
    }

    deleteUsuario(usuario: Usuario) {
        this.confirmationService.confirm({
            message: `¿Seguro que quieres eliminar al usuario "${usuario.nombre}"?`,
            header: 'Confirmar',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.usuarioService.delete(usuario.id).subscribe({
                    next: () => {
                        this.usuarios.set(this.usuarios().filter((u) => u.id !== usuario.id));
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Éxito',
                            detail: 'Usuario eliminado.',
                            life: 3000,
                        });
                    },
                    error: () =>
                        this.messageService.add({
                            severity: 'error',
                            summary: 'Error',
                            detail: 'No se pudo eliminar el usuario.',
                        }),
                });
            },
        });
    }

    guardarUsuario() {
        this.submitted = true;

        if (!this.usuario.nombre?.trim() || !this.usuario.correo?.trim()) {
            return;
        }

        if (this.esEdicion && this.usuario.id) {
            this.usuarioService.update(this.usuario.id, this.usuario.nombre, this.usuario.correo).subscribe({
                next: () => {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Éxito',
                        detail: 'Usuario actualizado.',
                        life: 3000,
                    });
                    this.usuarioDialog = false;
                    this.cargarUsuarios();
                },
                error: () =>
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'No se pudo actualizar el usuario.',
                    }),
            });
        } else {
            if (!this.contrasena.trim()) {
                return;
            }

            this.usuarioService.create(this.usuario.nombre, this.usuario.correo, this.contrasena).subscribe({
                next: () => {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Éxito',
                        detail: 'Usuario creado.',
                        life: 3000,
                    });
                    this.usuarioDialog = false;
                    this.cargarUsuarios();
                },
                error: () =>
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'No se pudo crear el usuario.',
                    }),
            });
        }
    }
}