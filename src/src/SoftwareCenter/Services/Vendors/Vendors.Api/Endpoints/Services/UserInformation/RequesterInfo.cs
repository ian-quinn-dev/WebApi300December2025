namespace Vendors.Api.Endpoints.Services.UserInformation;

public record RequesterInfo
{
    public required Guid Id { get; init; }
    public required string Sub { get; init; }
    public string[]? Roles { get; init; }
}