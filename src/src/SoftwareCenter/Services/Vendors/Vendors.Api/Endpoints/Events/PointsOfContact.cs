using Vendors.Api.Endpoints.Operations;

namespace Vendors.Api.Endpoints.Events;

public record VendorPointOfContactCreated(Guid Id, VendorPointOfContact PointOfContact);