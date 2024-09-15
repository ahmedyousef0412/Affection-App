import { CanComponentDeactivate } from './../../../shared/models/canComponentDeactivate ';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmEmailComponent } from '../confirm-email/confirm-email.component';
import { ForgetPasswordComponent } from '../forget-password/forget-password.component';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { ResetPasswordComponent } from '../reset-password/reset-password.component';
import { prventUnsavedGuardGuard } from '../../../shared/guards/prvent-unsaved-guard.guard';

const routes: Routes = [
  
  {path: '', redirectTo: 'login', pathMatch: 'full', },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent, canDeactivate:[prventUnsavedGuardGuard] },
  { path: 'auth/confirm-email', component: ConfirmEmailComponent },
  { path: 'forgetPassword', component: ForgetPasswordComponent },
  { path: 'auth/forget-password', component: ResetPasswordComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
