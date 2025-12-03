import { HttpContextToken, HttpEvent, HttpEventType, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { patchState, signalStore, withComputed, withMethods } from '@ngrx/signals';
import { addEntity, removeEntity, withEntities } from '@ngrx/signals/entities';
import { inject } from '@angular/core';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { Observable, tap } from 'rxjs';
type TrackedHttpRequestEntity = {
  id: string;
  method: string;
  url: string;
  timestamp: number;
}
type TrackedHttpRequestCreate = Omit<TrackedHttpRequestEntity, 'timestamp'>;
const trackingIdToken = new HttpContextToken<string>(() => crypto.randomUUID());

export const resourceActivityStore = signalStore(
  withDevtools('Resource Activity Store'),
  withEntities<TrackedHttpRequestEntity>(),
  withMethods((store) => ({
    trackRequest: (request: TrackedHttpRequestCreate) => patchState(store, addEntity({
      ...request,
      timestamp: Date.now(),
    })),
    trackResponse: (requestId: string) => patchState(store, removeEntity(requestId)),
  })),
  withComputed((store) => ({
    activeRequests: () => store.entities().length,
  })),
);

export function activityRequestInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const store = inject(resourceActivityStore);
  if (req instanceof HttpRequest) {
    console.log(`HTTP Request - Method: ${req.method}, URL: ${req.urlWithParams}`);
    const id = crypto.randomUUID();
    req.context.set(trackingIdToken, id);
    store.trackRequest({
      id,
      method: req.method,
      url: req.urlWithParams,
    });
  }
  return next(req);
}

export function activityResponseInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const store = inject(resourceActivityStore);

  return next(req).pipe(
    tap((event) => {
      if (event.type === HttpEventType.Response) {
        const id = req.context.get(trackingIdToken);
        if (id) {
          store.trackResponse(id);
        }
      }
    }),
  )
}
