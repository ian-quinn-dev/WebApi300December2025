import {
  CreateVendorRequestModel,
  postApiSoftwareVendors,
} from '../../api-clients/software';
import { Client, createClient } from '../../api-clients/software/client';
import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';

export class VendorsApi {
  #httpClient = inject(HttpClient);

  client: Client;

  constructor() {
    this.client = createClient({
      httpClient: this.#httpClient,
    });
  }

  postVendor(args: CreateVendorRequestModel) {
    return postApiSoftwareVendors({ client: this.client, body: args });
  }
}
