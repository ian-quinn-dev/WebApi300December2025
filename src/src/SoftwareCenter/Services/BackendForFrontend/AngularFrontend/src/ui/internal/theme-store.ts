import {
  patchState,
  signalStore,
  watchState,
  withHooks,
  withMethods,
  withProps,
  withState,
} from '@ngrx/signals';

const themeList = [
  'frappe',
  'latte',
  'macchiato',
  'mocha',
] as const;

type Theme = (typeof themeList)[number];

export const themeStore = signalStore(
  withState({ theme: 'light' as Theme }),
  withProps(() => {
    return {
      themes: themeList,
    };
  }),
  withMethods((store) => {
    return {
      set: (theme: Theme) => {
        patchState(store, { theme });
      },
    };
  }),
  withHooks({
    onInit(store) {
      const saved = localStorage.getItem('theme');

      if (saved && themeList.includes(saved as Theme)) {
        patchState(store, { theme: saved as Theme });
      }
      watchState(store, (state) => {
        document.documentElement.setAttribute('data-theme', state.theme);
        localStorage.setItem('theme', state.theme);
      });
    },
  }),
);
