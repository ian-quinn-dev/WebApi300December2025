using System.ComponentModel.DataAnnotations;
using Facet;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Vendors.Api.Endpoints.ReadModels;
using Vendors.Api.Endpoints.Services.UserInformation;
using Vendors.Api.Validations;
using Wolverine;

namespace Vendors.Api.Endpoints.Operations;

public static class Post
{
    private static readonly Guid FakeManager = Guid.Parse("5beb0a1a-191b-4c28-b886-f9b76b4e2dc5");
    public static async Task<Created<CreateVendorResponseModel>> PerformAddVendorAsync(
        CreateVendorRequestModel request,
        //[LoadUserInfo] RequesterInfo userInfo,
        IMessageBus messageBus)
    {
        var command = CreateVendor.FromRequest(request, FakeManager);

        await messageBus.PublishAsync(command);

        return TypedResults.Created("$/vendors/{command.Id}", new CreateVendorResponseModel(command));
    }


    extension(RouteGroupBuilder vendorGroup)
    {
        public RouteGroupBuilder MapPostVendor()
        {
            vendorGroup.MapPost("/", PerformAddVendorAsync)
                .WithTags("Vendors")
                .WithSummary("POST /vendors")
                .WithDescription("Allows a manager to add a new vendor to the system.");

            
            return vendorGroup;
        }
    }
}

public record VendorItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

}
[AnyOf(nameof(PointOfContactEmail), nameof(PointOfContactPhone))]
public record CreateVendorRequestModel : IValidatableObject
{
    [MinLength(10)] [MaxLength(50)] public required string Name { get; init; }

    [MinLength(10)] [MaxLength(500)]
    public required string Description { get; init; }

   
    public required string PointOfContactName { get; init; }
    public string PointOfContactEmail { get; init; } = string.Empty;
    public string PointOfContactPhone { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(PointOfContactEmail) && string.IsNullOrWhiteSpace(PointOfContactPhone))
            yield return new ValidationResult("Please specify either Email or Phone number",
            [
                nameof(PointOfContactEmail), nameof(PointOfContactPhone)
            ]);
    }
}

/// <summary>
///     What is returned to the caller when a new vendor is created
/// </summary>
/// <param name="Id">The Id of the newly created vendor</param>
/// <param name="ManagerId">The id of the manager that created the vendor</param>
/// <param name="Name">The name of the vendor created</param>
/// <param name="Description">A description of the vendor</param>
/// <param name="PointOfContact">The Point of Contact Information</param>
[Facet(typeof(CreateVendor))]
public partial record CreateVendorResponseModel
{
}

// Command
public record CreateVendor
{
    public required Guid Id { get; init; }
    public required Guid ManagerId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required VendorPointOfContact PointOfContact { get; init; }

    public static CreateVendor FromRequest(CreateVendorRequestModel request, Guid managerId)
    {
        return new CreateVendor
        {
            Id = Guid.NewGuid(),
            ManagerId = managerId,
            Name = request.Name,
            Description = request.Description,
            PointOfContact = new VendorPointOfContact(Guid.NewGuid())
            {
                Name = request.PointOfContactName,
                Email = request.PointOfContactEmail,
                Phone = request.PointOfContactPhone
            }
        };
    }
}

public record VendorPointOfContact(Guid Id)
{
    public required string Name { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
}