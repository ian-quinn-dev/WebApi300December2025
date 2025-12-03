import { HttpClient } from '@angular/common/http';
import { DOCUMENT, inject } from '@angular/core';

export type User = {
  name: string | null;
  isAuthenticated: boolean;
  claims: { type: string; value: string }[];
}
export class AuthApi {
  #client = inject(HttpClient);

  #document = inject(DOCUMENT);

  getUser() {
    // fake classroom stuff.
    return this.#client.get<User>('/bff/user');
  }

  logOut() {
    this.#document.location.href = `/bff/logout?returnUrl=${encodeURIComponent('/')}`;
  }

  login() {
    return this.#client.get('/bff/login?returnUrl=/', { observe: 'response', responseType: 'text' }).subscribe((resp) => {
      if (resp.url != null) {
        window.location.href = resp.url;
      }
    });
  }
}
