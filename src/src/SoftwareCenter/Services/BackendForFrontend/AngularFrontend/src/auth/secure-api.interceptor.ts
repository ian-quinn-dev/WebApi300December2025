import { getCookie } from './get-cookie';
import { HttpHandlerFn, HttpRequest } from '@angular/common/http';

export function secureApiInterceptor(
  request: HttpRequest<unknown>,
  next: HttpHandlerFn,
) {
  const secureRoutes = [getApiUrl('api'), getApiUrl('bff')];

  if (!secureRoutes.find((x) => request.url.startsWith(x))) {
    return next(request);
  }

  request = request.clone({
    headers: request.headers.set(
      'X-XSRF-TOKEN',
      getCookie('XSRF-RequestToken'),
    ),
  });

  return next(request);
}

function getApiUrl(path: string) {
  const backendHost = getCurrentHost();

  return `${backendHost}/{path}/`;
}

function getCurrentHost() {
  const host = globalThis.location.host;
  const url = `${globalThis.location.protocol}//${host}`;
  return url;
}
