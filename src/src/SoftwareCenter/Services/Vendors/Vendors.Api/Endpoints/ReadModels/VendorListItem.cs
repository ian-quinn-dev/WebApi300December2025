using Marten.Events.Aggregation;
using Vendors.Api.Endpoints.Events;
using Vendors.Api.Endpoints.Operations;

namespace Vendors.Api.Endpoints.ReadModels;

public record VendorListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }= string.Empty;
   public string Description { get; set; }= string.Empty;
}

public class VendorListProjection : SingleStreamProjection<VendorListItem, Guid>
{

    public static VendorListItem Create(VendorCreated evt) => new VendorListItem
    {
        Id = evt.Id,
        Name = evt.Name,
        Description = evt.Description
    };
}