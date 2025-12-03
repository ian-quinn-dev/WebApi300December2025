import {
  patchState,
  signalStore,
  watchState,
  withHooks,
  withMethods,
  withState,
} from '@ngrx/signals';

export const drawerStore = signalStore(
  withState({
    open: true,
  }),
  withMethods((store) => {
    return {
      toggle: () => {
        patchState(store, { open: !store.open() });
      },
    };
  }),
  withHooks({
    onInit(store) {
      const saved = localStorage.getItem('drawerOpen');

      if (saved) {
        const isOpen = saved === 'true';

        patchState(store, { open: isOpen });
      }
      watchState(store, (state) => {
        localStorage.setItem('drawerOpen', state.open.toString());
      });
    },
  }),
);
