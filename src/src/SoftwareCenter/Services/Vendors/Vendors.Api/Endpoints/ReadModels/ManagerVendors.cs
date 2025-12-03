using JasperFx.Events;
using Vendors.Api.Endpoints.Events;
using Vendors.Api.Endpoints.Services.UserInformation;

namespace Vendors.Api.Endpoints.ReadModels;

public record ManagerVendorModel(Guid Id, string Name);

public record ManagerVendors
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public IList<ManagerVendorModel> Vendors { get; init; } = [];
    public DateTimeOffset CreatedAt { get; init; }

    public static ManagerVendors Create(IEvent<ManagerCreated> @event)
    {
        return new ManagerVendors
        {
            Id = @event.Id,
            CreatedAt = @event.Timestamp
        };
    }

    public static ManagerVendors Apply(ManagerCreatedVendor @event, ManagerVendors model)
    {
        return model with { Vendors = [..model.Vendors, new ManagerVendorModel(@event.VendorId, @event.VendorName)] };
    }
}