import eslint from '@eslint/js';
import sheriff from '@softarc/eslint-plugin-sheriff';
import stylistic from '@stylistic/eslint-plugin';
import angular from 'angular-eslint';
import { defineConfig, globalIgnores } from 'eslint/config';
import { configs as tseslint } from 'typescript-eslint';

export default defineConfig([
  globalIgnores([
    '.angular/**',
    'src/mockServiceWorker.js',
    'dist/**',
    '**/*.gen.ts',
    '**/client/**',
  ]),
  {
    files: ['**/*.html'],
    plugins: {
      '@softarc/sheriff': sheriff,
      '@stylistic': stylistic,
      angular: angular.templatePlugin,
    },
    languageOptions: {
      parser: angular.templateParser,
    },
  },
  {
    files: ['**/*.ts'],
    extends: [
      eslint.configs.recommended,
      tseslint.strict,
      angular.configs.tsRecommended,
      stylistic.configs.recommended,
      sheriff.configs.all,
    ],

    processor: angular.processInlineTemplates,
    rules: {
      '@typescript-eslint/no-invalid-void-type': ['off'],
      '@stylistic/quote-props': ['off'],
      '@stylistic/arrow-parens': ['error', 'always'],
      '@stylistic/lines-between-class-members': ['error', 'always'],
      '@stylistic/array-bracket-newline': ['error'],
      '@stylistic/semi': ['off'],
      '@stylistic/member-delimiter-style': ['off'],
      '@typescript-eslint/naming-convention': [
        'error',
        {
          selector: 'variable',
          modifiers: ['const'],
          format: ['camelCase'],
        },
      ],
      '@typescript-eslint/no-extraneous-class': 'off',
      '@typescript-eslint/consistent-type-definitions': 'off',
      '@angular-eslint/directive-selector': [
        'error',
        {
          type: 'attribute',
          prefix: 'app',
          style: 'camelCase',
        },
      ],
      '@angular-eslint/component-selector': [
        'error',
        {
          type: 'element',
          prefix: 'app',
          style: 'kebab-case',
        },
      ],
    },
  },
]);
