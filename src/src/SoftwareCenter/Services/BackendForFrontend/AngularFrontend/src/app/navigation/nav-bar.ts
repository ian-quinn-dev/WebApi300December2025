import { drawerStore } from 'app-ui/drawer-store';
import { NavigationLink } from 'app-ui/navigation-link';
import { NavLink } from 'app-ui/types';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  isDevMode,
  signal,
} from '@angular/core';
import { AuthState } from '../../auth/auth-state';
import { injectDispatch } from '@ngrx/signals/events';
import { authEvents } from '../../auth/auth-events';
import { ErrorDisplay } from './error-display';
import { resourceActivityStore } from '../resource-tracking/store';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav-bar',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NavigationLink, ErrorDisplay, TitleCasePipe],
  template: `
  <app-error-display />



  <div
    class="drawer h-screen min-h-screen w-screen min-w-screen"
    [class.drawer-open]="store.open()"
  >
    <input
      id="my-drawer-3"
      type="checkbox"
      class="drawer-toggle"
      [checked]="store.open()"
    />
    <div class="drawer-content">
      <ng-content></ng-content>
    </div>
    <div class="drawer-side bg-base-200 shadow-accent h-full shadow-md">




      <div class="bg-base-300 flex flex-col h-16 w-full items-center justify-center">

        <div
          class="text-accent max-w-[90%] overflow-hidden text-sm font-extrabold text-ellipsis whitespace-nowrap"
          >
           @if(appRequestStore.activeRequests()) {
            <span class="absolute  left-4 loading loading-spinner loading-xs"></span>
          }
          {{ appTitle() }}</div
        >


 </div>



      <ul class="w-40 p-2 pt-6 text-sm">
        @for (link of prodLinks(); track link.label) {
          <li class="mb-2">
            <app-ui-navigation-link [link]="link" />
          </li>
        }
      </ul>
      <p class="font-bold text-sm px-2 ">DevMode</p>
       <ul class="w-40 p-2 pt-6 text-sm">
        @for (link of devLinks(); track link.label) {
          <li class="mb-2">
            <app-ui-navigation-link [link]="link" />
          </li>
        }
      </ul>
      <div
        class="absolute bottom-4 flex h-fit w-full flex-col content-start p-1"
      >



        @if (authState.isAuthenticated()) {
          <div class="tooltip" data-tip="Click To Log Out">
          <button
            class="btn btn-xs btn-info w-full rounded-none"
            (click)="logOut()"
          >
            <span class="text-xs">Hello, {{ authState.userName() | titlecase}}</span>
          </button>
          </div>
        } @else {
          <button class="btn btn-xs w-full rounded-none" (click)="logIn()">
            <span class="text-xs">Log In</span>
            @if(authState.isLoginPending()) {
              <span class="loading loading-spinner loading-xs"></span>
            }
          </button>
        }
      </div>

    </div>
  </div>`,
  styles: ``,
})
export class NavBar {
  protected appTitle = signal('Software Center');

  protected appRequestStore = inject(resourceActivityStore);

  protected authDispatcher = injectDispatch(authEvents);

  protected store = inject(drawerStore);

  protected authState = inject(AuthState);

  constructor() {
    this.authDispatcher.checkAuth();
  }

  protected logIn() {
    this.authDispatcher.loginRequested();
  }

  protected logOut() {
    this.authDispatcher.logoutRequested();
  }

  protected prodLinks = signal<NavLink[]>([{ label: 'Home', link: '/', exact: false, icon: 'home' }, { label: 'Vendors', link: '/vendors', exact: false, icon: 'info' }]);

  protected devLinks = signal<NavLink[]>([
    { label: 'Todo', link: '/todo', exact: false, icon: 'dev' },
    { label: 'Docs', link: '/docs', exact: false, icon: 'dev' },
  ]);

  protected links = computed(() => {
    if (isDevMode()) {
      return [...this.prodLinks(), ...this.devLinks()];
    }
    else {
      return this.prodLinks();
    }
  })
}
