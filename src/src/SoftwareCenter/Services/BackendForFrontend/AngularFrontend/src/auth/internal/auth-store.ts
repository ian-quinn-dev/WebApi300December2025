import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { withAuthEffects } from './auth-effects';
import { withAuthReducers } from './auth-reducer';
import { computed } from '@angular/core';
import { signalStore, withComputed } from '@ngrx/signals';

export const authStore = signalStore(
  withAuthEffects(),
  withAuthReducers(),
  withDevtools('auth'),

  withComputed((store) => ({
    isLoggedIn: computed(() => store.user()?.isAuthenticated === true),
    userName: computed(() => store.user()?.name),
    isSoftwareCenterEmployee: computed(() => {
      const user = store.user();
      if (!user || !user.isAuthenticated) {
        return false;
      }
      const roleClaims = user.claims.filter((c) => c.type === 'role').map((c) => c.value.toLowerCase());
      return roleClaims.includes('softwarecenter');
    }),
    isSoftwareCenterManager: computed(() => {
      const user = store.user();
      if (!user || !user.isAuthenticated) {
        return false;
      }
      const roleClaims = user.claims.filter((c) => c.type === 'role').map((c) => c.value.toLowerCase());
      return roleClaims.includes('softwarecenter') && roleClaims.includes('manager');
    }),
  })),
);
