import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { MessageModule } from 'primeng/message';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    PasswordModule,
    MessageModule
  ],
  template: `
    <div class="surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden">
      <div class="flex flex-column align-items-center justify-content-center">
        <div style="border-radius:56px; padding:0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%);">
          <div class="w-full surface-card py-8 px-5 sm:px-8" style="border-radius:53px; max-width: 450px;">
            <div class="text-center mb-5">
              <div class="text-900 text-3xl font-medium mb-3">Bienvenido</div>
              <span class="text-600 font-medium">Inicia sesión en IdeasGroup Scrum</span>
            </div>

            <form [formGroup]="form" (ngSubmit)="onSubmit()">
              <div class="mb-4">
                <label for="correo" class="block text-900 font-medium mb-2">Correo Electrónico</label>
                <input id="correo" type="text" pInputText formControlName="correo" class="w-full p-3" placeholder="ejemplo@ideasgroup.com" />
              </div>

              <div class="mb-4">
                <label for="contrasena" class="block text-900 font-medium mb-2">Contraseña</label>
                <p-password id="contrasena" formControlName="contrasena" [toggleMask]="true" [feedback]="false" styleClass="w-full" inputStyleClass="w-full p-3"></p-password>
              </div>

              <p-message *ngIf="errorMessage" severity="error" [text]="errorMessage" styleClass="w-full mb-4 block"></p-message>

              <button pButton pRipple label="Ingresar" type="submit" [loading]="loading" [disabled]="form.invalid" class="w-full p-3 text-xl"></button>
            </form>
          </div>
        </div>
      </div>
    </div>
  `
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loading = false;
  errorMessage = '';

  form = this.fb.group({
    correo: ['', [Validators.required, Validators.email]],
    contrasena: ['', [Validators.required]]
  });

  onSubmit(): void {
    if (this.form.invalid) return;

    this.loading = true;
    this.errorMessage = '';

    this.authService.login(this.form.value as any).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/tablero']);
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err.error?.message || 'Credenciales inválidas. Intenta de nuevo.';
      }
    });
  }
}