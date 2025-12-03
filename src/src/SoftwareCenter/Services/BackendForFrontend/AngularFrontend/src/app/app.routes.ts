import { AuthState } from '../auth/auth-state';
import { inject, isDevMode } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  RouterStateSnapshot,
  Routes,
} from '@angular/router';

const isManagerGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot,
) => {
  const authService = inject(AuthState);
  return authService.isSoftwareCenterManager();
};
const prodRoutes: Routes = [
  {
    path: '',
    title: 'Landing Page',
    loadChildren: () =>
      import('../landing/landing.routes').then((m) => m.landingRoutes),
  },
  {
    path: 'vendors',
    title: 'Vendors',
    canActivate: [isManagerGuard],
    loadChildren: () =>
      import('../vendors/vendors.routes').then((m) => m.vendorRoutes),
  },
];

const devRoutes: Routes = [];
if (isDevMode()) {
  // Add Devmode / Feature Gated Routes here
}

export const routes: Routes = [...prodRoutes, ...devRoutes];
