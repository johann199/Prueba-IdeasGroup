import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PrimeNGConfig } from 'primeng/api';
import { SelectButtonModule } from 'primeng/selectbutton';
import { LayoutService } from '../service/layout.service';

@Component({
    selector: 'app-configurator',
    standalone: true,
    imports: [CommonModule, FormsModule, SelectButtonModule],
    template: `
        <div class="flex flex-col gap-4">
            <div *ngIf="showMenuModeButton()" class="flex flex-col gap-2">
                <span class="text-sm text-muted-color font-semibold">Modo de Menú</span>
                <p-selectbutton 
                    [ngModel]="menuMode()" 
                    (ngModelChange)="onMenuModeChange($event)"  
                    optionLabel="label"
                    optionValue="value"
                ></p-selectbutton>
            </div>
        </div>
    `,
    host: {
        class: 'hidden absolute top-13 right-0 w-72 p-4 bg-surface-0 dark:bg-surface-900 border border-surface rounded-border origin-top shadow-md'
    }
})
export class AppConfigurator {
    router = inject(Router);
    primengConfig = inject(PrimeNGConfig);
    layoutService = inject(LayoutService);

    showMenuModeButton = signal(!this.router.url.includes('auth'));

    menuModeOptions = [
        { label: 'Static', value: 'static' },
        { label: 'Overlay', value: 'overlay' }
    ];

    menuMode() {
        return this.layoutService.layoutConfig().menuMode;
    }

    onMenuModeChange(event: string) {
        if (event) {
            this.layoutService.layoutConfig.update((prev: any) => ({ ...prev, menuMode: event }));
        }
    }
}