import { CreateVendor } from './create-vendor';
import { VendorsApi } from './internal/api-client';
import { Vendors } from './vendors';
import { Routes } from '@angular/router';

export const vendorRoutes: Routes = [
  {
    path: '',
    component: Vendors,
    providers: [VendorsApi],
    children: [
      {
        path: 'create',
        title: 'Create Vendor',
        component: CreateVendor,
      },
    ],
  },
];
