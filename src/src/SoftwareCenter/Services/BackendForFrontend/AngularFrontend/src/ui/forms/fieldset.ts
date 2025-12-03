import { Component, ChangeDetectionStrategy, input } from '@angular/core';

@Component({
  selector: 'app-ui-forms-fieldset',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  template: `
<fieldset class="fieldset">
  <legend class="fieldset-legend">{{ legend() }}</legend>
  <ng-content></ng-content>
</fieldset>
  `,
  styles: ``,
})
export class Fieldset {
  legend = input.required<string>();
}
