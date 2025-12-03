import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-ui-card-title',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  template: `
    <h2 class="card-title">
      <ng-content></ng-content>
    </h2>
  `,
  styles: ``,
})
export class CardTitle {}
