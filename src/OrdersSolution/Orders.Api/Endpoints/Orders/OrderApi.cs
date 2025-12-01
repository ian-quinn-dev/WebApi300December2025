using Orders.Api.Endpoints.Orders.Services;
using System.ComponentModel.DataAnnotations;

namespace Orders.Api.Endpoints.Orders
{
    public static class OrderApi
    {
        extension (IServiceCollection services)
        {
            public IServiceCollection AddOrders()
            {
                services.AddScoped<CardProcessor>();
                return services;
            }
        }
        extension (IEndpointRouteBuilder builder)
        {
            public IEndpointRouteBuilder MapOrders()
            {
                var ordersGroup = builder.MapGroup("/orders");

                ordersGroup.MapGet("/", () => "Your Orders");

                ordersGroup.MapPost("/", async (ShoppingCartRequest request, CardProcessor cardProcessor, CancellationToken token) =>
                {
                    // That transaction list
                    // validate the request

                    // do the early bound stuff we can do, then schedule the rest for later
                    // come back to this..
                    //arrange shipping
                    var shippingTask = Task.Delay(1000); // come back to this list when this is done
                    // charge card

                    //async tasks are not "fire and forget" - they are "fire and hope nothing bad happens"
                    var cardTask = cardProcessor.ProcessCardAsync(request.CustomerName, token);
                    // etc.
                    // save it...
                    // then
                    var order = new Order
                    {
                        Id = Guid.NewGuid(),
                        Total = request.Amount * 1.13M
                    };
                    return TypedResults.Ok(order); // the caller only gets this.
                });

                return builder;
            }
        }
    }

    public record ShoppingCartRequest
    {
        []
        public decimal Amount { get; set; }
        
        [Required, MinLength(3), MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;
    }

    public record Order
    {
        public Guid Id { get; set; }
        public decimal  Total { get; set; }
    }
}
