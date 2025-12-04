using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Vendors.Api.Endpoints.ReadModels;

namespace Vendors.Api.Endpoints.Operations;

public static  class GetVendors
{
    public static async Task<Ok<IReadOnlyList<VendorListItem>>> GetVendorsAsync(IDocumentSession session)
    {
        var vendors = await session.Query<VendorListItem>().ToListAsync();

        return TypedResults.Ok(vendors);
    }
    
}