import { Component, ChangeDetectionStrategy, signal, inject } from '@angular/core';
import { Field, form, schema, validateStandardSchema } from '@angular/forms/signals'

export const createVendorSchema = schema<CreateVendorRequestModel>((path) => validateStandardSchema(path, zCreateVendorRequestModel));
import { VendorsApi } from './internal/api-client';
import { CreateVendorRequestModel } from '../api-clients/software';
import { zCreateVendorRequestModel } from '../api-clients/software/zod.gen';
@Component({
  selector: 'app-vendor-create',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [Field],
  template: `
    <form novalidate (submit)="handleSubmit($event)">
    <fieldset class="fieldset">
        <legend class="fieldset-legend">What's the Vendor's Name?</legend>
          <input id="name" type="text" class="input" [field]="form.name" />
          <p class="label">Required</p>
          @if(form.name().invalid() && (form.name().touched() || form.name().dirty()  )) {
            <div class="alert alert-warning" role="alert">
              @for(error of form.name().errors(); track error) {
                <p class="error"> {{error.message}}</p>
              }
            </div>
      }

      </fieldset>

      <fieldset class="fieldset">
        <legend class="fieldset-legend">Description</legend>
        <input id="description" type="text" class="input" [field]="form.description" />
          <p class="label">Required</p>
      </fieldset>
      <fieldset class="fieldset">
        <legend class="fieldset-legend">Point of Contact Name:</legend>
        <input id="pocName" type="text" class="input" [field]="form.pointOfContactName" />
      </fieldset>
      <fieldset class="fieldset">
        <legend class="fieldset-legend">Point of Contact Email:</legend>
        <input id="pocEmail" type="text" class="input" [field]="form.pointOfContactEmail!" />
      </fieldset>
      <fieldset class="fieldset">
        <legend class="fieldset-legend">Point of Contact Phone:</legend>
        <input id="pocPhone" type="text" class="input" [field]="form.pointOfContactPhone!" />
      </fieldset>

      <button type="submit" [disabled]="form().submitting()" class="btn btn-lg btn-primary" >Create Vendor</button>

    </form>
  `,
  styles: ``,
})
export class CreateVendor {
  vendor = signal<CreateVendorRequestModel>({
    name: '',
    description: '',
    pointOfContactName: '',
    pointOfContactEmail: '',
    pointOfContactPhone: '',
  });

  form = form(this.vendor, createVendorSchema);

  #vendors = inject(VendorsApi)

  async handleSubmit(event: Event): Promise<void> {
    event.preventDefault();
    this.form().markAsTouched();
    if (this.form().invalid()) {
      return;
    }
    const response = await this.#vendors.postVendor(this.form().value());
    console.log(response);
  }
}
