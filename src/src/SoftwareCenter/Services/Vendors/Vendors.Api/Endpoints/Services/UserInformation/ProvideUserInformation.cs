using Marten;

namespace Vendors.Api.Endpoints.Services.UserInformation;

public record ManagerCreated;

public record SoftwareCenterMemberCreated;

public class ProvideUserInformation(IDocumentSession session)
{
    public async Task<RequesterInfo> LoadAsync(CancellationToken token = default)
    {
        var userSub = "user-sub-123"; // This would typically come from the authenticated user's claims
        var roles = new[] { "SoftwareCenter", "Manager" };

        var userRecord = await session.Query<RequesterInfo>()
                             .FirstOrDefaultAsync(u => u.Sub == userSub, token);

        if (userRecord != null) return userRecord;

        var requester = new RequesterInfo
        {
            Id = Guid.Parse("99EDCE20-5361-4A4A-BB4A-75980730FB80"),
            Sub = userSub,
            Roles = roles
        };
        session.Store(requester);

        if (requester.Roles.Any(IsSoftwareCenterEmployee))
            session.Events.StartStream<SoftwareCenterMemberCreated>(requester.Id, new SoftwareCenterMemberCreated());

        if (requester.Roles
            .Select(IsSoftwareCenterManager)
            .Any())
            session.Events.StartStream<ManagerCreated>(requester.Id, new ManagerCreated());
        await session.SaveChangesAsync(token);
        return requester;

        bool IsSoftwareCenterEmployee(string r)
        {
            return r == "SoftwareCenter";
        }

        bool IsSoftwareCenterManager(string r)
        {
            return r == "Manager" && IsSoftwareCenterEmployee(r);
        }
    }
}