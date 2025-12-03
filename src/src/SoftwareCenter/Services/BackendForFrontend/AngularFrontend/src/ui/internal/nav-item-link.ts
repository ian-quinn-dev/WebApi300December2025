import { NavLink } from 'app-ui/types';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav-item-link',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink, RouterLinkActive],
  template: ` <a
    [routerLink]="l().link"
    [routerLinkActiveOptions]="{ exact: link().exact ?? false }"
    #rla="routerLinkActive"
    [class.opacity-60]="rla.isActive === false"
    [routerLinkActive]="['underline', 'font-bold']"
    >{{ link().label }}</a
  >`,
  styles: ``,
})
export class NavItemLink {
  link = input.required<NavLink>();

  l = computed(() => this.link());
}
