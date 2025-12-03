using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using SoftwareCenter.Core.OpenApiDocumentTransforms;

namespace Catalog.Api.Infra;

public class CatalogOpenApiTransform : SoftwareCenterOAuth2DocumentTransformer
{
    public override IDictionary<string, string> NeededScopes { get; set; } = new Dictionary<string, string>
    {
        { "catalog.api", "Access the catalog API" },
        { "openid", "Access the OpenID Connect user profile" }
    };

    public override OpenApiInfo Info { get; set; } = new()
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "API for managing the software catalog"
    };
}

public class CatalogBffPathTransformer : BffPathTransformer
{
    public override string PathPrefix => "/api/catalog";
}