namespace Orders.Api.Endpoints.Cart;

public static class CartApi
{
    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapCart()
        {
            var group = routes.MapGroup("/cart");
            group.MapGet("/", () => "Cart Lives Here");

            return routes;
        }
    }
}
