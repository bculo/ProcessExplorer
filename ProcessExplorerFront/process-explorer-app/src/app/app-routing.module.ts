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
  { 
    path: 'admin', 
    loadChildren: () => import('./admin/admin.module').then(l => l.AdminModule) 
  },
  { 
    path: 'application', 
    loadChildren: () => import('./application/application.module').then(l => l.ApplicationModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
