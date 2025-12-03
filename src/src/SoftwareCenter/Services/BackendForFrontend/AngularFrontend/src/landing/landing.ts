import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { FeatureNav } from 'app-ui/feature-nav';
import { AuthState } from '../auth/auth-state';

@Component({
  selector: 'app-landing',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [FeatureNav],
  template: `
    <app-feature-nav sectionName="Angular Starter App" [links]="[]">
    <div class="prose">
      <h2>About This Reference App</h2>
      <p>This is a reference app and a starter app for Angular development.</p>
      <h3>Features</h3>

      <p>Some of the key features include:</p>
      <h4>Developer Mode</h4>
      <p>While running this in <code>ng serve</code>, you can access the developer mode.</p>
      <p>This includes a section of navigation on the left that has links to some documentation.</p>
    @if(userStuff.isSoftwareCenterEmployee()) {
      <h4>Software Center Employee Access</h4>
    }
    @if(userStuff.isSoftwareCenterManager()) {
      <h4>Software Center Manager Access</h4>
    }
    </div>
    </app-feature-nav>
  `,
  styles: ``,
})
export class Landing {
  userStuff = inject(AuthState);
}
