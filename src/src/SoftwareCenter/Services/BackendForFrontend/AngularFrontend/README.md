# Angular Starter

## `.vscode` Folder

- `extensions.json` - has additional extensions I recommend for doing Angular development.
  - Angular Language Service (`angular.ng-template`). **Required**
  - Prettier - (`esbenp.prettier-vscode`) - code formatting.
  - ESLint - (`dbaeumer.vscode-eslint`) - Linting for JS/TS
  - Tailwind (`bradlc.vscode-tailwindcss`) - Tailwind Intellisense.
  - TS Error Translator (`mattpocock.ts-error-translator`) - gives hints about TypeScript and better error messages.

- `settings.json` - Various settings I prefer for working in Angular.

- `tasks.json` - I have a run option so that when you open this directory, it automatically starts `ng serve -o`.

- `typescript.code-snippets`
  - Has two code snippets for creating Angular components.
  - `ngc` - Create an Angular Component
  - `ngrc` - Create an Angular Component, using the content of your clipboard for the template for the component.

## Additional NPM Packages and Configuration

- _CSS Framework_: [Tailwind](https://tailwindcss.com/docs/installation/framework-guides/angular) - forms, typography, autoprefixer, daisyui, postcss, tailwindcss
  - Also including `@tailwindcss/forms` and `@tailwindcss/typography`
  - _UI Library_: [DaisyUi](https://daisyui.com/)
- [Mock Service Workers](https://mswjs.io/) - for creating test doubles for API access.
- [Angular EsLint](https://github.com/angular-eslint/angular-eslint)
- [Prettier](https://prettier.io/)

## Using This

Create a directory, and in that directory:

```sh
npx degit hypertheorytraining/angular-starter
npm i
code .
```
