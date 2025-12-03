import { authEffectEvents } from '../auth-events';
import { authEvents } from '../auth-events';
import { signalStoreFeature, withState } from '@ngrx/signals';
import { on, withReducer } from '@ngrx/signals/events';
import { User } from './auth-api';

type AuthState = {
  user: User | null;
  pendingLogin: boolean;
};

export function withAuthReducers() {
  return signalStoreFeature(
    withState<AuthState>({
      user: null,
      pendingLogin: false,
    }),
    withReducer(
      on(authEvents.loginRequested, (state) => ({
        ...state,
        pendingLogin: true,
      })),

      on(authEffectEvents.loginSucceeded, ({ payload }) => ({
        user: { ...payload },
        pendingLogin: false,
      })),
      on(authEffectEvents.logoutSucceeded, () => ({ user: null, pendingLogin: false })),
    ),
  );
}
