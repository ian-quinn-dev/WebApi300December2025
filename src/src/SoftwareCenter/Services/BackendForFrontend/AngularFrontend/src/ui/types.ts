import { RouterLink } from '@angular/router';

type Link = RouterLink['routerLink'];

export type NavLinkEntry = {
  label: string;
  link: Link;
  icon: 'none' | 'home' | 'dev' | 'user' | 'info';
  requiredAuthenticatedUser?: boolean;
  exact?: boolean;
};

export type NavLink = NavLinkEntry;
