using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace SoftwareCenter.Core.OpenApiDocumentTransforms;

public abstract class BffPathTransformer : IOpenApiDocumentTransformer
{
    public abstract string PathPrefix { get; }
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var newPaths = new OpenApiPaths();
        foreach (var newPath in document.Paths) newPaths.Add($"{PathPrefix}{newPath.Key}", newPath.Value);
        document.Paths = newPaths;
        return Task.CompletedTask;
    }
}