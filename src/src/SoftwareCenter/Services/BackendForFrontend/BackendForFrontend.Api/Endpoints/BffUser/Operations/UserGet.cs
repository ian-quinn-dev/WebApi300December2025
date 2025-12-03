using System.Security.Claims;
using BackendForFrontend.Api.Endpoints.BffUser.ReadModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BackendForFrontend.Api.Endpoints.BffUser.Operations;

public static class UserGet
{
    public static Ok<User> Handle(ClaimsPrincipal principal)
    {
        var user = principal switch
        {
            { Identity.IsAuthenticated: true } => new User
            {
                IsAuthenticated = true,
                Name = principal.FindFirstValue("sub"),
                Claims = principal.Claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            },
            _ => new User
            {
                IsAuthenticated = false,
                Name = null
            }
        };

        return TypedResults.Ok(user);
    }
}