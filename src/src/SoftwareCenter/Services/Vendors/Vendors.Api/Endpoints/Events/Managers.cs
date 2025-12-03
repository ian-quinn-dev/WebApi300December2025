namespace Vendors.Api.Endpoints.Events;

public record ManagerCreatedVendor(Guid ManagerId, Guid VendorId, string VendorName);