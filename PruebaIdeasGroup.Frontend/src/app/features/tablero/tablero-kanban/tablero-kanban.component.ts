import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { 
    CdkDragDrop, 
    DragDropModule, 
    moveItemInArray, 
    transferArrayItem 
} from '@angular/cdk/drag-drop';

import { ConfirmationService, MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

import { ProyectoService } from '../../../core/services/proyecto.service';
import { ColumnaService } from '../../../core/services/columna.service';
import { TareaService } from '../../../core/services/tarea.service';

import { Proyecto } from '../../../core/models/proyecto.model';
import { Columna } from '../../../core/models/columna.model';
import { Tarea, CreateTarea } from '../../../core/models/tarea.model';

interface ColumnaConTareas extends Columna {
    tareas: Tarea[];
}

@Component({
    selector: 'app-tablero-kanban',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        DragDropModule,
        ButtonModule,
        DialogModule,
        DropdownModule,
        InputTextModule,
        InputTextareaModule,
        TagModule,
        ToastModule,
        ConfirmDialogModule
    ],
    providers: [MessageService, ConfirmationService],
    template: `
        <p-toast></p-toast>
        <p-confirmDialog></p-confirmDialog>

        <!-- Contenedor Principal: Ocupa exactamente el alto disponible sin desbordar la página -->
        <div class="flex flex-col h-[calc(100vh-9rem)] overflow-hidden">
            
            <!-- Header & Toolbar del Tablero -->
            <div class="card mb-3 p-3 flex flex-col md:flex-row justify-between items-center gap-3 bg-surface-900 border border-surface-800 rounded-lg flex-shrink-0">
                <div class="flex items-center gap-3 w-full md:w-auto">
                    <label class="font-bold text-base text-surface-0">Proyecto:</label>
                    <p-dropdown
                        [options]="proyectos()"
                        [(ngModel)]="proyectoSeleccionado"
                        optionLabel="nombre"
                        placeholder="Selecciona un proyecto"
                        (onChange)="onProyectoChange()"
                        class="w-full md:w-72"
                    ></p-dropdown>
                </div>

                <div class="flex gap-2" *ngIf="proyectoSeleccionado">
                    <p-button label="Nueva Columna" icon="pi pi-plus" severity="secondary" size="small" (onClick)="openNuevaColumna()"></p-button>
                </div>
            </div>

            <!-- Area de Columnas (Kanban) con Scroll Horizontal contenido -->
            <div 
                class="flex gap-4 overflow-x-auto h-full pb-2 items-start" 
                *ngIf="proyectoSeleccionado"
                cdkDropList
                cdkDropListOrientation="horizontal"
                (cdkDropListDropped)="dropColumna($event)"
            >
                <!-- Columna -->
                <div
                    *ngFor="let col of columnasConTareas()"
                    cdkDrag
                    class="bg-surface-900 border border-surface-800 rounded-lg p-3 w-72 flex-shrink-0 flex flex-col max-h-full shadow-md"
                >
                    <!-- Header Columna -->
                    <div class="flex justify-between items-center mb-2 px-1 flex-shrink-0" cdkDragHandle>
                        <div class="flex items-center gap-2 cursor-grab">
                            <i class="pi pi-ellipsis-v text-surface-400 text-xs"></i>
                            <span class="font-bold text-sm text-surface-0">{{ col.nombre }}</span>
                        </div>
                        <div class="flex items-center gap-2">
                            <span class="text-xs text-surface-400 font-semibold">{{ col.tareas.length }}</span>
                            <p-button icon="pi pi-ellipsis-h" [text]="true" severity="secondary" size="small" (onClick)="deleteColumna(col)"></p-button>
                        </div>
                    </div>

                    <!-- Lista de Tareas con Scroll Vertical Independiente por Columna -->
                    <div
                        [id]="'col-' + col.id"
                        cdkDropList
                        [cdkDropListData]="col.tareas"
                        [cdkDropListConnectedTo]="connectedToIds"
                        (cdkDropListDropped)="dropTarea($event, col.id)"
                        class="flex flex-col gap-2 overflow-y-auto flex-1 p-1 pr-2 min-h-[100px]"
                    >
                        <!-- Tarjeta Tarea -->
                        <div
                            *ngFor="let tarea of col.tareas"
                            cdkDrag
                            class="bg-surface-800 border border-surface-700 p-3 rounded-md shadow-sm hover:border-surface-600 transition-all cursor-grab active:cursor-grabbing flex justify-between items-center group flex-shrink-0"
                        >
                            <div class="flex flex-col gap-1 w-full pr-2">
                                <span class="text-xs text-surface-100 font-medium leading-relaxed">{{ tarea.nombre }}</span>
                            </div>
                            <p-button 
                                icon="pi pi-trash" 
                                [text]="true" 
                                severity="danger" 
                                size="small"
                                class="opacity-0 group-hover:opacity-100 transition-opacity"
                                (onClick)="deleteTarea(tarea)"
                            ></p-button>
                        </div>
                    </div>

                    <!-- Botón + Add a card al final -->
                    <button 
                        (click)="openNuevaTarea(col.id)"
                        class="mt-2 w-full py-2 bg-surface-800 hover:bg-surface-700 text-surface-300 hover:text-surface-0 border border-surface-700 rounded-md text-xs font-semibold flex items-center justify-center gap-1 transition-colors flex-shrink-0"
                    >
                        <i class="pi pi-plus text-xs"></i> Add a card
                    </button>
                </div>
            </div>
        </div>

        <!-- Diálogos (Columna y Tarea) se mantienen exactamente igual -->
    `
    
    
})
export class TableroKanbanComponent implements OnInit {
    proyectos = signal<Proyecto[]>([]);
    proyectoSeleccionado: Proyecto | null = null;

    columnasConTareas = signal<ColumnaConTareas[]>([]);
    connectedToIds: string[] = [];

    columnaDialog = false;
    nuevaColumnaNombre = '';

    tareaDialog = false;
    nuevaTarea: Partial<CreateTarea> = { prioridad: 1 };
    columnaTargetId: number | null = null;

    constructor(
        private proyectoService: ProyectoService,
        private columnaService: ColumnaService,
        private tareaService: TareaService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {}

    ngOnInit() {
        this.cargarProyectos();
    }

    cargarProyectos() {
        this.proyectoService.getAll().subscribe({
            next: (data) => {
                this.proyectos.set(data);
                if (data.length > 0) {
                    this.proyectoSeleccionado = data[0];
                    this.cargarTablero();
                }
            }
        });
    }

    onProyectoChange() {
        this.cargarTablero();
    }

    cargarTablero() {
        if (!this.proyectoSeleccionado) return;

        this.columnaService.getByProyectoId(this.proyectoSeleccionado.id).subscribe({
            next: (cols) => {
                cols.sort((a, b) => a.ordenDentroProyecto - b.ordenDentroProyecto);

                const columnaPromises = cols.map((col) =>
                    this.tareaService.getByColumnaId(col.id).toPromise().then((tareas) => {
                        const tareasOrdenadas = (tareas || []).sort((a, b) => a.ordenDentroColumna - b.ordenDentroColumna);
                        return { ...col, tareas: tareasOrdenadas };
                    })
                );

                Promise.all(columnaPromises).then((resultado) => {
                    this.columnasConTareas.set(resultado);
                    this.connectedToIds = resultado.map((c) => 'col-' + c.id);
                });
            }
        });
    }

    // DROP TAREAS
    dropTarea(event: CdkDragDrop<Tarea[]>, targetColumnaId: number) {
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
            this.actualizarOrdenTareasLocal(event.container.data, targetColumnaId);
        } else {
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex
            );
            
            const tareaMovida = event.container.data[event.currentIndex];
            tareaMovida.columnaId = targetColumnaId;

            this.tareaService.update(tareaMovida.id, {
                nombre: tareaMovida.nombre,
                descripcion: tareaMovida.descripcion,
                prioridad: tareaMovida.prioridad,
                ordenDentroColumna: event.currentIndex + 1,
                columnaId: targetColumnaId
            }).subscribe({
                next: () => {
                    this.actualizarOrdenTareasLocal(event.container.data, targetColumnaId);
                }
            });
        }
    }

    actualizarOrdenTareasLocal(tareas: Tarea[], columnaId: number) {
        tareas.forEach((t, index) => {
            t.ordenDentroColumna = index + 1;
            this.tareaService.update(t.id, {
                nombre: t.nombre,
                descripcion: t.descripcion,
                prioridad: t.prioridad,
                ordenDentroColumna: t.ordenDentroColumna,
                columnaId: columnaId
            }).subscribe();
        });
    }

    // Drop de columnas
    dropColumna(event: CdkDragDrop<ColumnaConTareas[]>) {
        const cols = [...this.columnasConTareas()];
        moveItemInArray(cols, event.previousIndex, event.currentIndex);
        this.columnasConTareas.set(cols);

        // Actualizar el orden en la base de datos
        cols.forEach((col, index) => {
            col.ordenDentroProyecto = index + 1;
            this.columnaService.update(col.id, col.nombre, col.ordenDentroProyecto).subscribe();
        });
    }

    //COLUMNAS
    openNuevaColumna() {
        this.nuevaColumnaNombre = '';
        this.columnaDialog = true;
    }

    guardarColumna() {
        if (!this.nuevaColumnaNombre.trim() || !this.proyectoSeleccionado) return;

        const orden = this.columnasConTareas().length + 1;
        this.columnaService.create(this.nuevaColumnaNombre, orden, this.proyectoSeleccionado.id).subscribe({
            next: () => {
                this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Columna creada' });
                this.columnaDialog = false;
                this.cargarTablero();
            }
        });
    }

    deleteColumna(col: Columna) {
        this.confirmationService.confirm({
            message: `¿Eliminar columna "${col.nombre}"?`,
            accept: () => {
                this.columnaService.delete(col.id).subscribe({
                    next: () => {
                        this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Columna eliminada' });
                        this.cargarTablero();
                    }
                });
            }
        });
    }

    //TAREAS
    openNuevaTarea(columnaId: number) {
        this.columnaTargetId = columnaId;
        const col = this.columnasConTareas().find(c => c.id === columnaId);
        
        this.nuevaTarea = {
            nombre: '',
            descripcion: '',
            columnaId: columnaId,
            prioridad: 1,
            ordenDentroColumna: (col?.tareas.length || 0) + 1
        };
        this.tareaDialog = true;
    }

    guardarTarea() {
        if (!this.nuevaTarea.nombre?.trim() || !this.nuevaTarea.columnaId) return;

        this.tareaService.create(this.nuevaTarea as CreateTarea).subscribe({
            next: () => {
                this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Tarea creada' });
                this.tareaDialog = false;
                this.cargarTablero();
            }
        });
    }

    deleteTarea(tarea: Tarea) {
        this.confirmationService.confirm({
            message: `¿Eliminar tarea "${tarea.nombre}"?`,
            accept: () => {
                this.tareaService.delete(tarea.id).subscribe({
                    next: () => {
                        this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Tarea eliminada' });
                        this.cargarTablero();
                    }
                });
            }
        });
    }
}