
import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {  HttpClientModule} from '@angular/common/http';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NotfoundComponent } from './shared/notfound/notfound.component';
import { HomeComponent } from './shared/home/home.component';
import { NavComponent } from './shared/nav/nav.component';
import { ToastrModule } from 'ngx-toastr';

import { CoreModule } from './core.module';
import { CommonModule } from '@angular/common';
import { PaginationModule  } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { ConfirmDialogComponent } from './shared/confirm-dialog/confirm-dialog.component';




@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    NotfoundComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    HttpClientModule,
    CoreModule,
    CommonModule,
    PaginationModule.forRoot(),
    ButtonsModule,
  
  
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      timeOut: 6000,
      extendedTimeOut: 1000,
      progressBar: true,
      closeButton: true,
      preventDuplicates: true,
    }),
  ],
  
  // providers: [
  //   provideClientHydration(),
  //   provideHttpClient(withInterceptors([authInterceptor, errorInterceptor]), withFetch()),

  //   provideAnimationsAsync()
  // ],
  bootstrap: [AppComponent]
})
export class AppModule { }
