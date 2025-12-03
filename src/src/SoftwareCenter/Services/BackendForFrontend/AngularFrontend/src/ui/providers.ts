import { drawerStore } from './drawer-store';
import { themeStore } from './internal/theme-store';
import { EnvironmentProviders, makeEnvironmentProviders } from '@angular/core';

export function provideAppUi(): EnvironmentProviders {
  return makeEnvironmentProviders([
    { provide: drawerStore, useClass: drawerStore },
    { provide: themeStore, useClass: themeStore },
  ]);
}
