using Microsoft.OpenApi;
using SoftwareCenter.Core.OpenApiDocumentTransforms;

namespace BackendForFrontend.Api.Extensions.OpenApi;

public class BffOpenApiTransform : SoftwareCenterOAuth2DocumentTransformer
{
    public override IDictionary<string, string> NeededScopes { get; set; } = new Dictionary<string, string>
    {
        { "catalog.api", "Access the catalog API" },
        { "vednords.api", "Access the vendors API" },
        { "openid", "Access the OpenID Connect user profile" }
    };

    public override OpenApiInfo Info { get; set; } = new()
    {
        Title = "Backend for Frontend Api for Angular App",
        Version = "v1",
        Description = ""
    };
}

