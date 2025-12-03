import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { errorsStore } from '../../errors/errors-store';
import { injectDispatch } from '@ngrx/signals/events';
import { appErrorEvents } from '../../errors/error-events';

@Component({
  selector: 'app-error-display',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  template: `
  @if(errStore.hasUnhandledErrors()) {
    <div class="absolute top-0 right-0 p-4">
        @if(errStore.hasError()) {
            <div class="stack stack-top">
            @for(err of errStore.unhandledErrors(); track err.id ) {
              <div role="" class="alert alert-error">
                <span>{{ err.message }}</span>
                <button class="" (click)="errorDispatcher.clearError(err.id)">X</button>
              </div>
            }
        </div>
       }
    </div>
  }
  `,
  styles: ``,
})
export class ErrorDisplay {
  protected errStore = inject(errorsStore)

  protected errorDispatcher = injectDispatch(appErrorEvents);
}
