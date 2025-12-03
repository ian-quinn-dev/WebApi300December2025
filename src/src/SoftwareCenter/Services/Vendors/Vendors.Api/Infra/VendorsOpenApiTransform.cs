using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

using SoftwareCenter.Core.OpenApiDocumentTransforms;

namespace Vendors.Api.Infra;

public class VendorsOpenApiTransform : SoftwareCenterOAuth2DocumentTransformer
{
    public override IDictionary<string, string> NeededScopes { get; set; } = new Dictionary<string, string>
    {
        { "vendors.api", "Access the Vendors API" },
        { "openid", "Access the OpenID Connect user profile" }
    };

    public override OpenApiInfo Info { get; set; } = new()
    {
        Title = "Vendors API",
        Version = "v1",
        Description = "API for managing vendors"
    };
}


public class VendorsBffPathTransformer : BffPathTransformer
{
    public override string PathPrefix => "/api/vendors";
}