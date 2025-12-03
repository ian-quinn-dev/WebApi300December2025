import { EnvironmentProviders, makeEnvironmentProviders } from '@angular/core';
import { AuthState } from './auth-state';
import { AuthApi } from './internal/auth-api';
import { authStore } from './internal/auth-store';

export function provideAppAuth(): EnvironmentProviders {
  return makeEnvironmentProviders([
    authStore,
    { provide: AuthApi, useClass: AuthApi },
    { provide: AuthState, useClass: AuthState },
  ]);
}
