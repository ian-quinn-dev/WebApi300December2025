namespace Vendors.Api.Endpoints.Services.Entities;

/// <summary>
///     When applied to a parameter on a route, this will load the entity
/// </summary>
/// <param name="idParam">It will assume the param is a Guid called "id", if it is called something else, set it here.</param>
/// <param name="lockedForWriting">If set to true (default is false) this will begin a transaction.</param>
[AttributeUsage(AttributeTargets.Parameter)]
public class LoadEntityAttribute(string idParam = "id", bool lockedForWriting = false) : Attribute
{
    public string Id { get; private set; } = idParam;
    public bool LockedForWriting { get; private set; } = lockedForWriting;
}