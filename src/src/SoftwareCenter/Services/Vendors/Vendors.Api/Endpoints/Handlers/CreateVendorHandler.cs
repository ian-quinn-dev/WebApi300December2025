using Marten;
using Vendors.Api.Endpoints.Events;
using Vendors.Api.Endpoints.Operations;

namespace Vendors.Api.Endpoints.Handlers;

public static class CreateVendorHandler
{
    public static async Task Handle(CreateVendor command, IDocumentSession session)
    {
        session.Events.StartStream<VendorCreated>(command.Id, new VendorCreated(command.Id, command.ManagerId,
            command.Name, command.Description, command.PointOfContact));

        session.Events.StartStream<VendorPointOfContactCreated>(
            new VendorPointOfContactCreated(command.PointOfContact.Id, command.PointOfContact));

        session.Events.Append(command.ManagerId, new ManagerCreatedVendor(command.ManagerId, command.Id, command.Name));

        await session.SaveChangesAsync();
    }
}