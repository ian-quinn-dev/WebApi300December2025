using Vendors.Api.Endpoints.Operations;

namespace Vendors.Api.Endpoints.Events;

public record VendorCreated(Guid Id, Guid ManagerId, string Name, string Description, VendorPointOfContact Poc);