# Agent Guidelines for Angular Reference App

## Commands
- **Start dev**: `npm start` (serves at http://localhost:4200 with auto-open)
- **Build**: `npm run build`
- **Lint**: `npm run lint` (ESLint - run before committing)
- **Unit tests**: `npm run test:unit` (Vitest)
- **E2E tests**: `npm run test:system` (Playwright with UI)
- **Single test**: `npm run test:unit -- path/to/file.spec.ts` (Vitest single file)
- **Sheriff (module boundaries)**: `npm run sheriff:check`

## Code Style
- **TypeScript**: Strict mode enabled. Avoid `any`, use `unknown`. Prefer `type` over `interface`. Use `readonly` and `as const` liberally.
- **Components**: Always standalone (default, don't set `standalone: true`). Use `changeDetection: ChangeDetectionStrategy.OnPush`. Inline templates for small components. Prefer `protected` over `public`/`private` for properties. Component selector prefix: `app-` (kebab-case).
- **Modern Angular**: Use `input()`/`output()` functions, not decorators. Use `inject()` function, not constructor injection. Use `@if`/`@for`/`@switch` control flow, not `*ngIf`/`*ngFor`/`*ngSwitch`. NO `ngClass`/`ngStyle`, use class/style bindings. NO `@HostBinding`/`@HostListener`, use `host` object.
- **State**: Signals for local state, `computed()` for derived state. Use NgRx Signals when needed.
- **Imports**: Auto-sorted by Prettier (import order separation and specifier sorting enabled). Path alias `app-ui/*` maps to `./src/ui/*`.
- **Formatting**: Prettier with Tailwind plugin. No semicolons in stylistic rules.
- **Naming**: camelCase for const variables (enforced). Component/directive selectors use `app-` prefix.
- **NO COMMENTS**: Do not add comments unless explicitly requested.
