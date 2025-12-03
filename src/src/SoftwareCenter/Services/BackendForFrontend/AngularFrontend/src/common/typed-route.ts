import { Route } from '@angular/router';
type RouteData = {
  pageTitle: string;
}
type TypedRoute = {
  data: RouteData;
} & Route

export type TypedRoutes = TypedRoute[];
