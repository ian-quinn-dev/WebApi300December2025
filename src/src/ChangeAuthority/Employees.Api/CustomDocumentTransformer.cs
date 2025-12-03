using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Reflection.Metadata.Ecma335;

namespace Employees.Api
{
    public class CustomDocumentTransformer : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var infoBlock = new OpenApiInfo()
            {
                Title = "Super Employees Api 300",
                Contact = new OpenApiContact
                {
                    Name = "Jeff",
                    Email = "jeff@company.com"
                }
            };
            document.Info = infoBlock;



            return Task.CompletedTask;
        }
        
    }
}
