import { HttpClient, httpResource } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';
import { FeatureNav } from 'app-ui/feature-nav';
// import { client } from '../api-clients/software/client.gen';
import { JsonPipe } from '@angular/common';
import { VendorListItem } from '../api-clients/vendors';
@Component({
  selector: 'app-vendors',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [FeatureNav, JsonPipe],
  template: `
    <app-feature-nav sectionName="Vendor Management" [links]="[{
      label: 'Create Vendor', link: '/vendors/create', exact: false, icon: 'none' }
    ]">

    @if(vendorList.hasValue()) {
     @for(vendor of vendorList.value(); track vendor.id) {
        <div class="card mb-3">
          <div class="card-body">
            <h5 class="card-title">{{vendor.name}}</h5>
            <p class="card-text">{{vendor.description}}</p>

          </div>
        </div>
        <pre>{{vendor | json}}</pre>
      }
    }



    @if(vendorList.isLoading()) {
      <p>Getting your vendors...</p>
    }
    </app-feature-nav>
  `,
  styles: ``,
})
export class Vendors implements OnInit {
  vendorList = httpResource<VendorListItem[]>(() => '/api/vendors');
}
