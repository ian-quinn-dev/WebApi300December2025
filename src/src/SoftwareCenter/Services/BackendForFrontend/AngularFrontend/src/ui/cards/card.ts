import {
  booleanAttribute,
  ChangeDetectionStrategy,
  Component,
  input,
} from '@angular/core';

@Component({
  selector: 'app-ui-card',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  template: `
    <div
      class="card bg-base-200 border-2"
      [class.card-border]="showBorder()"
      [class.card-sm]="size() === 'sm'"
      [class.card-md]="size() === 'md'"
      [class.card-lg]="size() === 'lg'"
      [class.card-xl]="size() === 'xl'"
      [class.card-side]="imageOn() === 'side'"
      [class.card-top]="imageOn() === 'top'"
      [class.image-full]="imageOn() === 'full'"
    >
      <ng-content select="[card-image]"></ng-content>

      <div class="card-body">
        <h2 class="card-title">
          <ng-content select="[card-title]"></ng-content>
        </h2>

        <ng-content></ng-content>

        <div class="card-actions justify-end ">
          <ng-content  select="[card-actions]"></ng-content>
</div>

      </div>
    </div>
  `,
  styles: ``,
})
export class UiCard {
  showBorder = input(false, { transform: booleanAttribute });

  size = input<'sm' | 'md' | 'lg' | 'xl'>('md');

  imageOn = input<'side' | 'top' | 'full'>('top');
}
