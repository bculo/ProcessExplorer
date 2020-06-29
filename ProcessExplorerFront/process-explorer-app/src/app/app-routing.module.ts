import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/authentication' },
  { 
    path: 'authentication', 
    loadChildren: () => import('./authentication/authentication.module').then(l => l.AuthenticationModule) 
  },
  { 
    path: 'session', 
    loadChildren: () => import('./session/session.module').then(l => l.SessionModule) 
  },
  { 
    path: 'process', 
    loadChildren: () => import('./process/process.module').then(l => l.ProcessModule) 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
