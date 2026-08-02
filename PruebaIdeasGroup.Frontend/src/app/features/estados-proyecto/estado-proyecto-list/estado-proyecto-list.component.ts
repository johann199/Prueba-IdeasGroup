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
import { DialogModule } from 'primeng/dialog';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { EstadoProyectoService } from '../../../core/services/estado-proyecto.service';
import { EstadoProyecto } from '../../../core/models/estado-proyecto.model';

@Component({
    selector: 'app-estado-proyecto-list',
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
            [value]="estados()"
            [rows]="10"
            [paginator]="true"
            [globalFilterFields]="['nombre']"
            [tableStyle]="{ 'min-width': '50rem' }"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} estados"
            [showCurrentPageReport]="true"
            [rowsPerPageOptions]="[10, 20, 30]"
        >
            <ng-template pTemplate="caption">
    <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
        <h5 class="m-0 text-xl font-semibold">Estados de Proyecto</h5>
        
        <p-iconField iconPosition="right" class="w-full sm:w-auto">
            <p-inputIcon styleClass="pi pi-search"></p-inputIcon>
            <input 
                pInputText 
                type="text" 
                (input)="onGlobalFilter(dt, $event)" 
                placeholder="Buscar..." 
                class="w-full sm:w-80"
            />
        </p-iconField>
    </div>
</ng-template>

            <ng-template pTemplate="header">
                <tr>
                    <th pSortableColumn="nombre" style="min-width: 16rem">
                        Nombre
                        <p-sortIcon field="nombre"></p-sortIcon>
                    </th>
                    <th pSortableColumn="creado" style="min-width: 12rem">
                        Creado
                        <p-sortIcon field="creado"></p-sortIcon>
                    </th>
                    <th style="min-width: 8rem"></th>
                </tr>
            </ng-template>

            <ng-template pTemplate="body" let-estado>
                <tr>
                    <td style="min-width: 16rem">{{ estado.nombre }}</td>
                    <td style="min-width: 12rem">{{ estado.creado | date: 'dd/MM/yyyy HH:mm' }}</td>
                    <td>
                        <p-button icon="pi pi-pencil" class="mr-2" [rounded]="true" [outlined]="true" (click)="editEstado(estado)"></p-button>
                        <p-button icon="pi pi-trash" severity="danger" [rounded]="true" [outlined]="true" (click)="deleteEstado(estado)"></p-button>
                    </td>
                </tr>
            </ng-template>

            <ng-template pTemplate="emptymessage">
                <tr>
                    <td colspan="3" class="text-center py-4">No hay estados de proyecto todavía.</td>
                </tr>
            </ng-template>
        </p-table>

        <p-dialog [(visible)]="estadoDialog" [style]="{ width: '450px' }" header="Detalle del Estado" [modal]="true">
            <ng-template pTemplate="content">
                <div class="flex flex-column gap-3 pt-3">
                    <div>
                        <label for="nombre" class="block font-bold mb-2">Nombre</label>
                        <input type="text" pInputText id="nombre" [(ngModel)]="estado.nombre" required autofocus class="w-full" />
                        <small class="p-error" *ngIf="submitted && !estado.nombre">El nombre es obligatorio.</small>
                    </div>
                </div>
            </ng-template>

            <ng-template pTemplate="footer">
                <p-button label="Cancelar" icon="pi pi-times" [text]="true" (click)="hideDialog()"></p-button>
                <p-button label="Guardar" icon="pi pi-check" (click)="saveEstado()"></p-button>
            </ng-template>
        </p-dialog>

        <p-confirmDialog [style]="{ width: '450px' }"></p-confirmDialog>
    `,
    providers: [MessageService, ConfirmationService],
})
export class EstadoProyectoListComponent implements OnInit {
    estadoDialog: boolean = false;

    estados = signal<EstadoProyecto[]>([]);

    estado: Partial<EstadoProyecto> = {};

    submitted: boolean = false;

    @ViewChild('dt') dt!: Table;

    constructor(
        private estadoProyectoService: EstadoProyectoService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) { }

    ngOnInit() {
        this.cargarEstados();
    }

    cargarEstados() {
        this.estadoProyectoService.getAll().subscribe({
            next: (data) => this.estados.set(data),
            error: () =>
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'No se pudieron cargar los estados de proyecto.',
                }),
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.estado = {};
        this.submitted = false;
        this.estadoDialog = true;
    }

    editEstado(estado: EstadoProyecto) {
        this.estado = { ...estado };
        this.estadoDialog = true;
    }

    hideDialog() {
        this.estadoDialog = false;
        this.submitted = false;
    }

    deleteEstado(estado: EstadoProyecto) {
        this.confirmationService.confirm({
            message: `¿Seguro que quieres eliminar el estado "${estado.nombre}"?`,
            header: 'Confirmar',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.estadoProyectoService.delete(estado.id).subscribe({
                    next: () => {
                        this.estados.set(this.estados().filter((e) => e.id !== estado.id));
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Éxito',
                            detail: 'Estado eliminado.',
                            life: 3000,
                        });
                    },
                    error: () =>
                        this.messageService.add({
                            severity: 'error',
                            summary: 'Error',
                            detail: 'No se pudo eliminar el estado.',
                        }),
                });
            },
        });
    }

    saveEstado() {
        this.submitted = true;

        if (!this.estado.nombre?.trim()) {
            return;
        }

        const idExistente = this.estado.id;
        const nombre = this.estado.nombre!;

        if (idExistente) {
            // Edición
            this.estadoProyectoService.update(idExistente, nombre).subscribe({
                next: () => {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Éxito',
                        detail: 'Estado actualizado.',
                        life: 3000,
                    });
                    this.estadoDialog = false;
                    this.estado = {};
                    this.cargarEstados();
                },
                error: () =>
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'No se pudo actualizar el estado.',
                    }),
            });
        } else {
            // Creación
            this.estadoProyectoService.create(nombre).subscribe({
                next: () => {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Éxito',
                        detail: 'Estado creado.',
                        life: 3000,
                    });
                    this.estadoDialog = false;
                    this.estado = {};
                    this.cargarEstados();
                },
                error: () =>
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'No se pudo crear el estado.',
                    }),
            });
        }
    }
}