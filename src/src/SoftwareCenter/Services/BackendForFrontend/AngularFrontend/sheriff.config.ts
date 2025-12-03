import { SheriffConfig, sameTag } from '@softarc/sheriff-core';

export const config: SheriffConfig = {
  entryPoints: {
    app: './src/main.ts',
  },
  enableBarrelLess: true,
  autoTagging: true,
  ignoreFileExtensions: ['.gen.ts'],
  modules: {
    'src/ui': ['type:shared'],
    'src/auth': ['type:shared'],
    'src/errors': ['type:shared'],

    'src/common': ['type:shared'],
    'src/app/<domain>/<type>': ['domain:<domain>', 'type:<type>'],
  },
  depRules: {
    'domain:*': [sameTag, 'type:shared'],
    'type:feature': ['type:shared', 'noTag'],
    'type:shared': ['noTag', sameTag],
    noTag: ['noTag', 'root'],
    root: ['type:shared', 'noTag'],
  },
};
