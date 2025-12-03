using Vendors.Api.Endpoints.Operations;

namespace Vendors.Api.Endpoints;

public static class VendorApiExtensions
{
    extension<TBuilder>(TBuilder source) where TBuilder : IHostApplicationBuilder
    {
        public TBuilder AddVendors()
        {
            return source;
        }
    }

    extension(IEndpointRouteBuilder endpoints)
    {
        public IEndpointRouteBuilder MapVendors()
        {
            var vendorGroup = endpoints.MapGroup("/")
                .WithTags("Vendors");


            vendorGroup.MapPostVendor();
            return endpoints;
        }
    }
}