import { inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterStateSnapshot, TitleStrategy } from '@angular/router';

class AppTitleStrategy extends TitleStrategy {
  readonly title = inject(Title)

  constructor(private readonly defaultTitle: string = '') {
    super();
  }

  override updateTitle(routerState: RouterStateSnapshot) {
    const title = this.buildTitle(routerState);
    if (title !== undefined) {
      this.title.setTitle(`${this.defaultTitle} - ${title}`);
    }
    else {
      this.title.setTitle(this.defaultTitle);
    }
  }
}

export function provideAppTitleStrategy(defaultTitle?: string) {
  return {
    provide: TitleStrategy,
    useFactory: () => new AppTitleStrategy(defaultTitle),
  };
}
