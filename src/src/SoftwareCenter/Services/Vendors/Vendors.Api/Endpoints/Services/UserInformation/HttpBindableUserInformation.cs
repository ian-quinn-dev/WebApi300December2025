using System.Reflection;


namespace Vendors.Api.Endpoints.Services.UserInformation;

[AttributeUsage(AttributeTargets.Parameter)]
public class LoadUserInfoAttribute : Attribute
{

}

public class HttpBindableUserInformation<T> : IBindableFromHttpContext<T>
    where T : RequesterInfo, IBindableFromHttpContext<T>
{
    public static async ValueTask<T?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<T>>();
        var attribute = parameter.GetCustomAttributes(typeof(LoadUserInfoAttribute), true).SingleOrDefault();
        var provideUser = context.RequestServices.GetRequiredService<ProvideUserInformation>();
        switch (attribute)
        {
            case LoadUserInfoAttribute entityAttribute:
            {
                var userInfo = await provideUser.LoadAsync(context.RequestAborted);
                return (T)userInfo;

            }
            default:
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("Parameter {ParameterName} is not of type loadable", parameter.Name);
                }

                return null;
            }
        }
    }
}