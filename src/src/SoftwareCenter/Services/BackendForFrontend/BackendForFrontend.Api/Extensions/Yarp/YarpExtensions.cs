using BackendForFrontend.Api.Extensions.Yarp.Transforms;
using BackendForFrontend.Api.Extensions.Yarp.YarpTransformers;
using Yarp.ReverseProxy.Transforms;

namespace BackendForFrontend.Api.Extensions.Yarp;

public static class YarpExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder AddBffYarpReverseProxy()
        {
            // var envVar = Environment.GetEnvironmentVariable("FRONT_END");
            // var configToLoad = envVar == "dev" ? "ReverseProxyDev" : "ReverseProxyProd";
            // Console.WriteLine("Loading YARP config section: " + configToLoad);
            builder.Services.AddSingleton<AddBearerTokenToHeadersTransform>();
            builder.Services.AddSingleton<AddAntiforgeryTokenResponseTransform>();
            builder.Services.AddSingleton<ValidateAntiforgeryTokenRequestTransform>();
            builder.Services.AddSingleton<ReplaceAntiforgeryPlaceholderTransform>();


            builder.Services
                .AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddTransforms(builderContext =>
                {
                    builderContext.RequestTransforms.Add(
                        builderContext.Services.GetRequiredService<ValidateAntiforgeryTokenRequestTransform>());

                    builderContext.ResponseTransforms.Add(builderContext.Services
                        .GetRequiredService<AddAntiforgeryTokenResponseTransform>());

                    builderContext.ResponseTransforms.Add(builderContext.Services
                        .GetRequiredService<ReplaceAntiforgeryPlaceholderTransform>());

                    builderContext.RequestTransforms.Add(new RequestHeaderRemoveTransform("Cookie"));

                    if (!string.IsNullOrEmpty(builderContext.Route.AuthorizationPolicy) &&
                        builderContext.Route.AuthorizationPolicy != "Anonymous")
                        builderContext.RequestTransforms.Add(builderContext.Services
                            .GetRequiredService<AddBearerTokenToHeadersTransform>());
                })
                .AddServiceDiscoveryDestinationResolver();


            return builder;
        }
    }


    extension(HttpContext context)
    {
        public string BuildRedirectUrl(string? redirectUrl)
        {
            if (string.IsNullOrEmpty(redirectUrl)) redirectUrl = "/";
            if (redirectUrl.StartsWith('/'))
                redirectUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase +
                              redirectUrl;
            return redirectUrl;
        }
    }
}