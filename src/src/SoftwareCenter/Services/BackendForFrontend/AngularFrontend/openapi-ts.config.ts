import { defineConfig } from '@hey-api/openapi-ts';

export default defineConfig({
  input: ['./open-api-specs/Vendors.Api.json'],
  output: [
    {
      format: 'prettier',
      lint: 'eslint',
      path: './src/api-clients/vendors',
    },
  ],
  plugins: [
    'zod',
    {
      name: '@hey-api/client-angular',
      throwOnError: true,
    },
    '@hey-api/schemas',
    {
      asClass: true,
      name: '@hey-api/sdk',
    },
    {
      name: '@hey-api/sdk',
      validators: true,
    },
  ],
});
