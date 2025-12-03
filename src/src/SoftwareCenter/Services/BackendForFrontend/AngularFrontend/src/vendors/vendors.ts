import { HttpClient, httpResource } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';
import { FeatureNav } from 'app-ui/feature-nav';
import { client } from '../api-clients/software/client.gen';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-vendors',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [FeatureNav, JsonPipe],
  template: `
    <app-feature-nav sectionName="Vendor Management" [links]="[{
      label: 'Create Vendor', link: '/vendors/create', exact: false, icon: 'none' }
    ]">

    @if(vendorList.hasValue()) {
      <pre>{{ vendorList.value() | json}}</pre>
    }

    @if(vendorList.isLoading()) {
      <p>Getting your vendors...</p>
    }
    </app-feature-nav>
  `,
  styles: ``,
})
export class Vendors implements OnInit {
  #client = inject(HttpClient);

  vendorList = httpResource(() => '/api/vendors');

  ngOnInit(): void {
    client.setConfig({
      baseUrl: '/api',
      httpClient: this.#client,
    })
  }
}
