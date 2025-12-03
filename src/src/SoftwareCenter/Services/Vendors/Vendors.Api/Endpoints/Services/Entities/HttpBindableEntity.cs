using System.Reflection;
using Marten;

namespace Vendors.Api.Endpoints.Services.Entities;

public abstract class HttpBindableEntity<T> : IBindableFromHttpContext<T> where T : class, IBindableFromHttpContext<T>

{
    public static async ValueTask<T?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var session = context.RequestServices.GetRequiredService<IDocumentSession>();
        var logger = context.RequestServices.GetRequiredService<ILogger<T>>();
        var attribute = parameter.GetCustomAttributes(typeof(LoadEntityAttribute), true).SingleOrDefault();
        switch (attribute)
        {
            case LoadEntityAttribute entityAttribute:
            {
                var id = entityAttribute.Id;
                if (entityAttribute.LockedForWriting) session.BeginTransaction();

                var paramValue = context.Request.RouteValues[id];
                if (paramValue is null) return null;

                if (!Guid.TryParse(paramValue.ToString(), out var guid)) return null;
                var saved = await session.LoadAsync<T>(guid);

                if (saved is not null) return saved;
                context.Items.Add(new NotFoundItem(), typeof(T).Name);

                return null;
            }
            default:
            {
                if (logger.IsEnabled(LogLevel.Debug))
                    logger.LogDebug("Parameter {ParameterName} is not of type DooDad", parameter.Name);

                return null;
            }
        }
    }

    public T? GetEntity()
    {
        return this as T;
    }
}