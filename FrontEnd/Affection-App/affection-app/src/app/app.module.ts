import { errorInterceptor } from './core/interceptors/error.interceptor';
import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { ErrorInterceptor } from './core/services/error.interceptor';
import { LoginComponent } from './modules/auth/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './modules/auth/register/register.component';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HomeComponent } from './shared/home/home.component';
import { NavComponent } from './shared/nav/nav.component';
import { MembersComponent } from './modules/client/members/members.component';
import { authInterceptor } from './core/interceptors/auth.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ConfirmEmailComponent } from './modules/auth/confirm-email/confirm-email.component';
import { ForgetPasswordComponent } from './modules/auth/forget-password/forget-password.component';
import { ResetPasswordComponent } from './modules/auth/reset-password/reset-password.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NotfoundComponent,
    HomeComponent,
    NavComponent,
    MembersComponent,
    ConfirmEmailComponent,
    ForgetPasswordComponent,
    ResetPasswordComponent,
    
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    HttpClientModule,

    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,

    MatNativeDateModule,

    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right', 
      timeOut: 6000,                       
      extendedTimeOut: 1000,               
      progressBar: true,                   
      closeButton: true,                   
      preventDuplicates: true,             
    }), 
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withInterceptors([authInterceptor,errorInterceptor]),withFetch()),
   
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
