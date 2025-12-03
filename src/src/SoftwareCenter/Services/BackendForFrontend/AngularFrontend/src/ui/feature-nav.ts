import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
} from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { drawerStore } from './drawer-store';
import { NavigationLink } from './navigation-link';
import { ThemePicker } from './theme-picker';
import { NavLink } from './types';

@Component({
  selector: 'app-feature-nav',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterOutlet, RouterLink, NavigationLink, ThemePicker],
  template: `
    <div class="flex h-full flex-col ">
      <div class="bg-base-300 flex h-16 flex-row items-center">
        <button
          (click)="drawerStore.toggle()"
          class="btn btn-square btn-ghost"
          [class.-rotate-180]="drawerStore.open()"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="32"
            height="32"
            viewBox="0 0 24 24"
          >
            <!-- Icon from Unicons Thin Line by Iconscout - https://github.com/Iconscout/unicons/blob/master/LICENSE -->
            <path
              fill="#888888"
              d="M2.82 9.116a.5.5 0 0 0-.64.768L4.719 12l-2.54 2.116a.5.5 0 0 0 .641.768l3-2.5a.5.5 0 0 0 0-.768zM12.5 7h9a.5.5 0 0 0 0-1h-9a.5.5 0 0 0 0 1M9.045 5h-.003a.5.5 0 0 0-.5.497l-.084 13a.5.5 0 0 0 .497.503h.003a.5.5 0 0 0 .5-.497l.084-13A.5.5 0 0 0 9.045 5M21.5 10h-9a.5.5 0 0 0 0 1h9a.5.5 0 0 0 0-1m0 8h-9a.5.5 0 0 0 0 1h9a.5.5 0 0 0 0-1m0-4h-9a.5.5 0 0 0 0 1h9a.5.5 0 0 0 0-1"
            />
          </svg>
        </button>
        @if (sectionName()) {
          <a routerLink="." class="btn btn-ghost hover:link text-xl">{{
            sectionName()
          }}</a>
        }

        <ul class="menu menu-horizontal px-4">
          @for (link of links(); track link.link) {
            <li>
              <app-ui-navigation-link [link]="link" />
            </li>
          }
        </ul>
        <app-theme-picker class="mr-4 ml-auto" />
      </div>
      <div class=" p-4">

        <ng-content></ng-content>

      <div class=" p-4 w-full min-w-full">
        <router-outlet />
        </div>
      </div>
    </div>
  `,
  styles: ``,
})
export class FeatureNav {
  sectionName = input('');

  links = input.required<NavLink[]>();

  drawerStore = inject(drawerStore);
}
