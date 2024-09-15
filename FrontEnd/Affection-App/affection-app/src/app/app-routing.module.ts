import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { NotfoundComponent } from './shared/notfound/notfound.component';
import { HomeComponent } from './shared/home/home.component';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },


  {
    path: 'auth',
    loadChildren: () =>
      import('./modules/auth/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./modules/account/account/account.module').then((m) => m.AccountModule),
  },

  {

    path: 'client',
    loadChildren: () =>
      import('./modules/client/client/client.module').then((m) => m.ClientModule)
  },

  { path: 'Not-found', component: NotfoundComponent },
  { path: '**', component: NotfoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
