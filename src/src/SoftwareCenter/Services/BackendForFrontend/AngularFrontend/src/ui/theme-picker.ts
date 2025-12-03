import { themeStore } from 'app-ui/internal/theme-store';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
} from '@angular/core';

@Component({
  selector: 'app-theme-picker',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  template: `
    <div class="dropdown dropdown-bottom dropdown-left w-full">
      <div tabindex="0" role="button" class="">
        <span class="text-xs"
          ><svg
            xmlns="http://www.w3.org/2000/svg"
            width="32"
            height="32"
            viewBox="0 0 24 24"
          >
            <!-- Icon from MingCute Icon by MingCute Design - https://github.com/Richard9394/MingCute/blob/main/LICENSE -->
            <g fill="none" fill-rule="evenodd">
              <path
                d="m12.593 23.258l-.011.002l-.071.035l-.02.004l-.014-.004l-.071-.035q-.016-.005-.024.005l-.004.01l-.017.428l.005.02l.01.013l.104.074l.015.004l.012-.004l.104-.074l.012-.016l.004-.017l-.017-.427q-.004-.016-.017-.018m.265-.113l-.013.002l-.185.093l-.01.01l-.003.011l.018.43l.005.012l.008.007l.201.093q.019.005.029-.008l.004-.014l-.034-.614q-.005-.018-.02-.022m-.715.002a.02.02 0 0 0-.027.006l-.006.014l-.034.614q.001.018.017.024l.015-.002l.201-.093l.01-.008l.004-.011l.017-.43l-.003-.012l-.01-.01z"
              />
              <path
                fill="#888888"
                d="M20.477 3.511a3 3 0 0 1 0 4.243l-1.533 1.533a2.99 2.99 0 0 1-.581 3.41l-.715.714a2 2 0 0 1-2.828 0l-6.485 6.485a3 3 0 0 1-2.122.879h-1.8a1.2 1.2 0 0 1-1.2-1.2v-1.8a3 3 0 0 1 .88-2.122l6.484-6.485a2 2 0 0 1 0-2.828l.714-.714a2.99 2.99 0 0 1 3.41-.582l1.533-1.533a3 3 0 0 1 4.243 0m-8.485 7.071l-6.486 6.486l-.087.099a1 1 0 0 0-.206.608v1h1l.132-.009a1 1 0 0 0 .575-.284l6.486-6.485zm7.07-5.657a1 1 0 0 0-1.414 0L15.534 7.04a1.01 1.01 0 0 1-1.428 0a.99.99 0 0 0-1.4 0l-.714.714l4.242 4.243l.714-.715a.99.99 0 0 0 0-1.4a1.01 1.01 0 0 1 0-1.428l2.115-2.114a1 1 0 0 0 0-1.415Z"
              />
            </g></svg
        ></span>
      </div>
      <ul tabindex="-1" class="dropdown-content bg-base-300">
        @for (theme of themeList(); track theme.value) {
          <li>
            <input
              type="radio"
              name="theme-dropdown"
              class="theme-controller btn btn-sm btn-block btn-ghost w-fit justify-start"
              [aria-label]="theme.label"
              [value]="theme.value"
              (click)="store.set(theme.value)"
              [disabled]="theme.disabled"
            />
          </li>
        }
      </ul>
    </div>
  `,
  styles: ``,
})
export class ThemePicker {
  store = inject(themeStore);

  themeList = computed(() => {
    return this.store.themes.map((theme) => {
      return {
        label: this.toTitleCase(theme),
        value: theme,
        disabled: theme === this.store.theme(),
      };
    });
  });

  toTitleCase(str: string) {
    return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
  }
}
