import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HomeIcon } from './icons/home';
import { InfoIcon } from './icons/info';
import { DevIcon } from './icons/dev';
import { NavLink } from './types';

@Component({
  selector: 'app-ui-navigation-link',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [HomeIcon, RouterLinkActive, RouterLink, InfoIcon, DevIcon],
  template: `
    <a
      [routerLink]="link().link"
      [routerLinkActiveOptions]="{ exact: link().exact ?? false }"
      #rla="routerLinkActive"
      [class.opacity-60]="rla.isActive === false"
      [routerLinkActive]="['underline']"
      class="flex flex-row items-center gap-2"
    >
      <span class="flex h-6 flex-row items-center gap-2 text-xs">
        @switch (link().icon) {
          @case ('home') {
            <app-icons-home />
          }
          @case ('info') {
            <app-icon-info />
          }
          @case('dev') {
            <app-icon-dev />
          }
          @default {}
        }

        {{ link().label }}
      </span>
    </a>
  `,
  styles: ``,
})
export class NavigationLink {
  link = input.required<NavLink>();

  hasIcon = computed(
    () => this.link().icon !== 'none' && Boolean(this.link().icon),
  );
}
