import { ResetPasswordComponent } from './modules/auth/reset-password/reset-password.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/auth/login/login.component';
import { RegisterComponent } from './modules/auth/register/register.component';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { HomeComponent } from './shared/home/home.component';
import { MembersComponent } from './modules/client/members/members.component';
import { ConfirmEmailComponent } from './modules/auth/confirm-email/confirm-email.component';
import { ForgetPasswordComponent } from './modules/auth/forget-password/forget-password.component';

const routes: Routes = [

  {
    path: '', component: HomeComponent
  },
  {
    path: 'home'
    , component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'auth/confirm-email',
    component: ConfirmEmailComponent
  },
  {
    path: 'forgetPassword',
    component: ForgetPasswordComponent
  },
  {
    path: 'auth/forget-password',
    component: ResetPasswordComponent
  },
  {
    path: 'members',
    component: MembersComponent
  },
  { path: 'Not-found', component: NotfoundComponent },
  { path: '**', component: NotfoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
