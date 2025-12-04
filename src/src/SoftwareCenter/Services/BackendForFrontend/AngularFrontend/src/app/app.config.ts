import { client } from '../api-clients/vendors/client.gen';
import { provideHeyApiClient } from '../api-clients/vendors/client/client.gen';
import { provideAppAuth } from '../auth/providers';
import { secureApiInterceptor } from '../auth/secure-api.interceptor';
import { provideAppTitleStrategy } from '../common/app-title-strategy';
import { provideAppErrors } from '../errors/providers';
import { routes } from './app.routes';
import {
  activityRequestInterceptor,
  activityResponseInterceptor,
  resourceActivityStore,
} from './resource-tracking/store';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
  withXsrfConfiguration,
} from '@angular/common/http';
import {
  ApplicationConfig,
  CSP_NONCE,
  provideZonelessChangeDetection,
} from '@angular/core';
import {
  provideRouter,
  withEnabledBlockingInitialNavigation,
  withViewTransitions,
} from '@angular/router';
import {
  QueryClient,
  provideTanStackQuery,
} from '@tanstack/angular-query-experimental';
import { withDevtools } from '@tanstack/angular-query-experimental/devtools';
import { provideAppUi } from 'app-ui/providers';

const nonce = (
  document.querySelector('meta[name="CSP_NONCE"]') as HTMLMetaElement
)?.content;

export const appConfig: ApplicationConfig = {
  providers: [
    provideAppUi(),
    provideAppAuth(),
    provideAppErrors(),
    resourceActivityStore,
    provideZonelessChangeDetection(),
    provideRouter(
      routes,
      withViewTransitions(),
      withEnabledBlockingInitialNavigation(),
    ),
    provideHttpClient(
      withFetch(),
      withXsrfConfiguration({
        cookieName: '__SoftwareCenterBFF-X-XSRF-TOKEN',
        headerName: 'X-XSRF-TOKEN',
      }),
      withInterceptors([
        secureApiInterceptor,
        activityRequestInterceptor,
        activityResponseInterceptor,
      ]),
    ),
    provideTanStackQuery(new QueryClient(), withDevtools()),
    provideAppTitleStrategy('Software Center'),
    provideHeyApiClient(client),
    {
      provide: CSP_NONCE,
      useValue: nonce,
    },
  ],
};
