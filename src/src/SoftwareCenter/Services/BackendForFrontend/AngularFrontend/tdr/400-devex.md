# Developer Experience

We assume Visual Studio Code as our editor, though any editor can be used as long as it allows configuration to follow the practices agreed upon here.

## Visual Studio Code

The `.vscode` folder contains settings, and other instructions that will be "noticed" by VS Code when the folder containing this application is opened.

> Note: You must open the folder the _immediately_ contains the `.vscode` folder for this to work.

### Extensions

Developers can use whatever VS Code extensions they like, of course, but this project works best if you use the extensions listed in the [`.vscode/extension.json`](../.vscode/extensions.json) file.

#### Angular Language Service

Listed as "angular.ng-template", provides the Angular Language Service as well as the VSCode extension for templates and other Angular specific features.

#### Prettier

We have set in the `.vscode/settings.json` Prettier as the default formatter. This extension integrates with the Prettier formatter (which is installed as a package dependency).

#### ES Lint

#### Tailwind

#### Total TypeScript

Provides learning cues for TypeScript concepts (the blue squigglies), as well as some TypeScript error message translation.

#### Playwright

### Settings

The [`settings.json`](../.vscode/settings.json) file has settings that will override your settings while using this project.

### Snippets

In a `*.ts` file, the following snippets are available:

- `ngc` - Create an Angular component
- `ngrc` - Create an Angular component, and place the content of your clipboard in the template.
- `ngfr` - Create a feature route file.
