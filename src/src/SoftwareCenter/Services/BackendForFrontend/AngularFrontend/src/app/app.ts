import { Component } from '@angular/core';
import { NavBar } from './navigation/nav-bar';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
    <app-nav-bar>
      <router-outlet></router-outlet>
    </app-nav-bar>

  `,
  styles: [],
  imports: [NavBar, RouterOutlet],

})
export class App {

}
