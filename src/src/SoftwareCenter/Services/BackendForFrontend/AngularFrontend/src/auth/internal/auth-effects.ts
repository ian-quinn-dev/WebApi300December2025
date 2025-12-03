import { appErrorEvents } from '../../errors/error-events';
import { authEffectEvents, authEvents } from '../auth-events';
import { AuthApi } from './auth-api';
import { inject } from '@angular/core';
import { mapResponse } from '@ngrx/operators';
import { signalStoreFeature } from '@ngrx/signals';
import { Events, injectDispatch, withEffects } from '@ngrx/signals/events';
import { map, switchMap, tap } from 'rxjs';

export function withAuthEffects() {
  return signalStoreFeature(
    withEffects(
      (
        _,
        events = inject(Events),
        api = inject(AuthApi),
        errorEvents = injectDispatch(appErrorEvents),

      ) => ({
        login$: events.on(authEvents.loginRequested).pipe(
          tap(() => api.login()),
        ),
        checkAuth$: events.on(authEvents.checkAuth).pipe(
          switchMap(() =>
            api.getUser().pipe(
              mapResponse({
                next(value) {
                  if (value.isAuthenticated) {
                    return authEffectEvents.loginSucceeded(value);
                  }
                  else {
                    return authEffectEvents.logoutSucceeded();
                  }
                },
                error(e) {
                  return authEffectEvents.loginFailed({
                    errorMessage: 'Login Failed',
                    error: e,

                  });
                },
              }),
            ),
          ),
        ),
        handleLogout$: events.on(authEvents.logoutRequested).pipe(
          tap(() => api.logOut(),
          ),
        ),

        handleLoginFailed$: events.on(authEffectEvents.loginFailed).pipe(
          map((a) => {
            errorEvents.setError({
              error: `Could Not Log You In`,
              feature: 'auth',
              originalError: a.payload.error,
            });
            errorEvents.setError({
              error: `Please try again later.`,
              feature: 'auth',
            });
          },
          ),
        ),
      }),
    ),
  );
}
