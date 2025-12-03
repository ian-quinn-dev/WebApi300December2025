import { EnvironmentProviders, makeEnvironmentProviders } from '@angular/core';
import { errorsStore } from './errors-store';

export function provideAppErrors(): EnvironmentProviders {
  return makeEnvironmentProviders([{ provide: errorsStore, useClass: errorsStore }]);
}
