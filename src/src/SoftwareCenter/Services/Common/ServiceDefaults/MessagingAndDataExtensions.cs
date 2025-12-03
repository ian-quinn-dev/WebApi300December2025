using System.Reflection;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.Marten;

namespace ServiceDefaults;

public static class MessagingAndDataExtensions
{
    extension(IHostBuilder host)
    {
        private IHostBuilder UseWolverineForApi(Action<WolverineOptions>? configureWolverine)
        {
            var wolverineOptions = new WolverineOptions()
            {
                ServiceName = Assembly.GetCallingAssembly().GetName().Name ?? string.Empty,
            };
            configureWolverine?.Invoke(wolverineOptions);
           host.UseWolverine(configureWolverine);
           return host;
        }
    }

    extension(IServiceCollection services)
    {
        private MartenServiceCollectionExtensions.MartenConfigurationExpression AddMartenForApi(Action<StoreOptions>? configureMarten)
        {
            var martenOptions = new StoreOptions();
            configureMarten?.Invoke(martenOptions);
            return services.AddMarten(martenOptions)
                .UseLightweightSessions();

        }
    }
    extension(WebApplicationBuilder builder)
    {
        
        public WebApplicationBuilder AddMartenWithWolverine( 
            Action<StoreOptions>? configureMarten = null, 
            Action<WolverineOptions>? configureWolverine = null,
            bool integrateMartenWithWolverine = true,
            bool useNpgsqlDataSource = true
            )
        {
            
            if (OpenApiGen.IsBuildingOpenApiDocs()) return builder;
            

           
            builder.Host.UseWolverineForApi(configureWolverine);
            var martenConfig = builder.Services.AddMartenForApi(configureMarten);
            if (integrateMartenWithWolverine)
            {
                martenConfig.IntegrateWithWolverine();
            }

            if (useNpgsqlDataSource)
            {
                martenConfig.UseNpgsqlDataSource();
            }
            
            return builder;
        }
    }

   
}

internal static class OpenApiGen
{
    public static bool IsBuildingOpenApiDocs()
    {
        return Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";
    }

    public static bool NotBuildingOpenApiDocs()
    {
        return !IsBuildingOpenApiDocs();
    }
}