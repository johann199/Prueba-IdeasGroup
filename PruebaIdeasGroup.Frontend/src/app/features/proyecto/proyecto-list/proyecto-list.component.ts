import { Component, OnInit, signal, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// PrimeNG 17 Imports
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DialogModule } from 'primeng/dialog';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

import { ProyectoService } from '../../../core/services/proyecto.service';
import { Proyecto, CreateProyecto, UpdateProyecto } from '../../../core/models/proyecto.model';

@Component({
    selector: 'app-proyecto-list',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        TableModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        InputTextModule,
        InputTextareaModule,
        DialogModule,
        InputIconModule,
        IconFieldModule,
        ConfirmDialogModule
    ],
    template: `
        <p-toast></p-toast>

        <p-toolbar styleClass="mb-4">
            <ng-template pTemplate="start">
                <p-button label="Nuevo Proyecto" icon="pi pi-plus" (onClick)="openNew()"></p-button>
            </ng-template>
        </p-toolbar>

        <p-table
            #dt
            [value]="proyectos()"
            [rows]="10"
            [paginator]="true"
            [globalFilterFields]="['nombre', 'descripcion']"
            [tableStyle]="{ 'min-width': '50rem' }"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} proyectos"
            [showCurrentPageReport]="true"
            [rowsPerPageOptions]="[10, 20, 30]"
        >
            <ng-template pTemplate="caption">
                <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
                    <h5 class="m-0 text-xl font-semibold">Proyectos</h5>

                    <p-iconField iconPosition="right" class="w-full sm:w-auto">
                        <p-inputIcon styleClass="pi pi-search"></p-inputIcon>
                        <input
                            pInputText
                            type="text"
                            (input)="onGlobalFilter(dt, $event)"
                            placeholder="Buscar por nombre o descripción..."
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
                    <th pSortableColumn="descripcion" style="min-width: 18rem">
                        Descripción
                        <p-sortIcon field="descripcion"></p-sortIcon>
                    </th>
                    <th pSortableColumn="creado" style="min-width: 12rem">
                        Fecha Creación
                        <p-sortIcon field="creado"></p-sortIcon>
                    </th>
                    <th style="min-width: 8rem">Acciones</th>
                </tr>
            </ng-template>

            <ng-template pTemplate="body" let-proyecto>
                <tr>
                    <td style="min-width: 14rem" class="font-medium">{{ proyecto.nombre }}</td>
                    <td style="min-width: 18rem">{{ proyecto.descripcion || 'Sin descripción' }}</td>
                    <td style="min-width: 12rem">{{ proyecto.creado | date: 'dd/MM/yyyy HH:mm' }}</td>
                    <td>
                        <p-button icon="pi pi-pencil" class="mr-2" [rounded]="true" [outlined]="true" (click)="editProyecto(proyecto)"></p-button>
                        <p-button icon="pi pi-trash" severity="danger" [rounded]="true" [outlined]="true" (click)="deleteProyecto(proyecto)"></p-button>
                    </td>
                </tr>
            </ng-template>

            <ng-template pTemplate="emptymessage">
                <tr>
                    <td colspan="4" class="text-center py-4">No hay proyectos registrados aún.</td>
                </tr>
            </ng-template>
        </p-table>

        <!-- Diálogo para Crear / Editar Proyecto -->
        <p-dialog [(visible)]="proyectoDialog" [style]="{ width: '450px' }" header="Detalle del Proyecto" [modal]="true">
            <ng-template pTemplate="content">
                <div class="flex flex-col gap-4 pt-3">
                    <div>
                        <label for="nombre" class="block font-bold mb-2">Nombre del Proyecto</label>
                        <input type="text" pInputText id="nombre" [(ngModel)]="proyecto.nombre" required autofocus class="w-full" />
                        <small class="p-error" *ngIf="submitted && !proyecto.nombre?.trim()">El nombre es obligatorio.</small>
                    </div>

                    <div>
                        <label for="descripcion" class="block font-bold mb-2">Descripción</label>
                        <textarea
                            id="descripcion"
                            pInputTextarea
                            [(ngModel)]="proyecto.descripcion"
                            rows="4"
                            class="w-full"
                            placeholder="Añade una descripción breve..."
                        ></textarea>
                    </div>
                </div>
            </ng-template>

            <ng-template pTemplate="footer">
                <p-button label="Cancelar" icon="pi pi-times" [text]="true" (click)="hideDialog()"></p-button>
                <p-button label="Guardar" icon="pi pi-check" (click)="guardarProyecto()"></p-button>
            </ng-template>
        </p-dialog>

        <p-confirmDialog [style]="{ width: '450px' }"></p-confirmDialog>
    `,
    providers: [MessageService, ConfirmationService]
})
export class ProyectoListComponent implements OnInit {
    proyectoDialog: boolean = false;
    proyectos = signal<Proyecto[]>([]);
    proyecto: Partial<Proyecto> = {};
    esEdicion: boolean = false;
    submitted: boolean = false;

    @ViewChild('dt') dt!: Table;

    constructor(
        private proyectoService: ProyectoService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {}

    ngOnInit() {
        this.cargarProyectos();
    }

    cargarProyectos() {
        this.proyectoService.getAll().subscribe({
            next: (data) => this.proyectos.set(data),
            error: () =>
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'No se pudieron cargar los proyectos.'
                })
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.proyecto = {};
        this.esEdicion = false;
        this.submitted = false;
        this.proyectoDialog = true;
    }

    editProyecto(proyecto: Proyecto) {
        this.proyecto = { ...proyecto };
        this.esEdicion = true;
        this.submitted = false;
        this.proyectoDialog = true;
    }

    hideDialog() {
        this.proyectoDialog = false;
        this.submitted = false;
    }

    deleteProyecto(proyecto: Proyecto) {
        this.confirmationService.confirm({
            message: `¿Seguro que quieres eliminar el proyecto "${proyecto.nombre}"?`,
            header: 'Confirmar Eliminación',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.proyectoService.delete(proyecto.id).subscribe({
                    next: () => {
                        this.proyectos.set(this.proyectos().filter((p) => p.id !== proyecto.id));
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Éxito',
                            detail: 'Proyecto eliminado.',
                            life: 3000
                        });
                    },
                    error: () =>
                        this.messageService.add({
                            severity: 'error',
                            summary: 'Error',
                            detail: 'No se pudo eliminar el proyecto.'
                        })
                });
            }
        });
    }
    guardarProyecto() {
    this.submitted = true;

    if (!this.proyecto.nombre?.trim() || !this.proyecto.descripcion?.trim()) {
        return;
    }

    if (this.esEdicion && this.proyecto.id) {
        const updateDto: UpdateProyecto = {
            nombre: this.proyecto.nombre,
            descripcion: this.proyecto.descripcion,
            fechaInicio: this.proyecto.fechaInicio ?? new Date().toISOString(),
            fechaFin: this.proyecto.fechaFin ?? new Date().toISOString(),
            estadoId: this.proyecto.estadoId ?? 1
        };

        this.proyectoService.update(this.proyecto.id, updateDto).subscribe({
            next: () => {
                this.messageService.add({
                    severity: 'success',
                    summary: 'Éxito',
                    detail: 'Proyecto actualizado.',
                    life: 3000
                });
                this.proyectoDialog = false;
                this.cargarProyectos();
            },
            error: (err) =>
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: err.message || 'No se pudo actualizar el proyecto.'
                })
        });
    } else {
        const createDto: CreateProyecto = {
            nombre: this.proyecto.nombre,
            descripcion: this.proyecto.descripcion,
            fechaInicio: this.proyecto.fechaInicio ?? new Date().toISOString(),
            fechaFin: this.proyecto.fechaFin ?? new Date().toISOString(),
            creadoPorId: this.proyecto.creadoPorId ?? 1, // ID del usuario autenticado
            estadoId: this.proyecto.estadoId ?? 1
        };

        this.proyectoService.create(createDto).subscribe({
            next: () => {
                this.messageService.add({
                    severity: 'success',
                    summary: 'Éxito',
                    detail: 'Proyecto creado.',
                    life: 3000
                });
                this.proyectoDialog = false;
                this.cargarProyectos();
            },
            error: (err) =>
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: err.message || 'No se pudo crear el proyecto.'
                })
        });
    }
}

}