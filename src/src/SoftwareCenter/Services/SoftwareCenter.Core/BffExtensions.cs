using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using ServiceDefaults.OpenApiDocumentTransforms;
using SoftwareCenter.Core.OpenApiDocumentTransforms;

namespace SoftwareCenter.Core;

public static class OpenApiRelatedExtensions
{
    private const string CorsPolicyName = "ForScalar";
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Since we are using Scalar, we need to allow CORS during development to be able to test OpenAPI docs with the
        ///     Swagger UI.
        ///     This method adds a permissive CORS policy when not building OpenAPI docs.
        /// </summary>
        /// <returns></returns>
 

        /// <summary>
        ///     This adds OpenAPI with document transformers for BFF scenario and for local dev/testing.
        /// </summary>
        /// <param name="pathPrefix">
        ///     As dictated by the bff, usually "/api/{path}". This will be used in an additional tranform to
        ///     change the paths of your endpoints.
        /// </param>
        /// <param name="version">For the service OpenApi document</param>
        /// <typeparam name="TTransformer">Your transformer that adds scopes, and Info.</typeparam>
        /// <returns></returns>
        public IServiceCollection AddSoftwareCenterOpenApiWithTransforms<TTransformer>(
            string version) where TTransformer : SoftwareCenterOAuth2DocumentTransformer
        {
           services.AddOpenApi(version, config => config.AddDocumentTransformer<TTransformer>());
            return services;
        }
        public IServiceCollection AddSoftwareCenterOpenApiWithTransformsForBff(
            string version, string pathPrefixForBff) 
        {


            return services;
        }
        
    }

    extension(WebApplication app)
    {
        /// <summary>
        ///     This maps OpenAPI endpoints during development only.
        /// </summary>
        /// <returns></returns>
        public WebApplication MapSoftwareCenterOpenApiDuringDevelopment()
        {
            // if (app.Environment.IsDevelopment())
            // {
            //     app.UseCors(CorsPolicyName);
            //     app.MapOpenApi("/openapi/{document}")
            //         .AllowAnonymous();
            //     app.MapOpenApi("/dev/vendors/openapi/{documentName}.json").AllowAnonymous();
            // }
            //
            // return app;
            return app;
        }
    }

    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddSoftwareCenterCorsForOpenApiDuringDevelopment()
        {
            

            return builder;
        }
    }
}
