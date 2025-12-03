using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppHost;

public static class IdentityCommandExtensions
{
    private static async Task<ExecuteCommandResult> OnGetResultForCommand(HttpCommandResultContext context)
    {
        var response = await context.Response.Content.ReadAsStringAsync(context.CancellationToken);
        var data = JsonSerializer.Deserialize<AccessTokenResult>(response);
        if (data is null) return new ExecuteCommandResult { Success = false, ErrorMessage = "Couldn't Get The Token" };

        var token = data.AccessToken;

        var (sub, roles) = GetSubAndRolesFromToken(token);

        var roleList = roles.Count > 0 ? string.Join(", ", roles) : "No Roles";
        Console.WriteLine($"SUB: {sub}");
        Console.WriteLine("ROLES: " + roleList);

        Console.WriteLine("Token:");
        Console.WriteLine(token);

        return new ExecuteCommandResult { Success = true };
    }

    private static void ModifyRequest(HttpRequestMessage request, string sub)
    {
        Console.WriteLine("TOKEN FOR: " + sub);
        request.Content =
            new StringContent(
                $"grant_type=client_credentials&client_id=software&client_secret=secret&scope=api.read&audience=software-center-api&code={sub}\n");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    }

    public static (string, IList<string>) GetSubAndRolesFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var sub = jwtToken.Subject ?? string.Empty;
        var roles = new List<string>();

        if (jwtToken.Payload.TryGetValue("role", out var rolesObj) &&
            rolesObj is JsonElement { ValueKind: JsonValueKind.Array } rolesElement)
            roles.AddRange(rolesElement.EnumerateArray().Select(role => role.GetString() ?? string.Empty));

        return (sub, roles);
    }

    extension(IResourceBuilder<ProjectResource> builder)
    {
        public IResourceBuilder<ProjectResource> WithIdentityOpenIdAuthority(
            IResourceBuilder<ContainerResource> identity)
        {
            builder.WithEnvironment("Authentication__Schemes__OpenIdConnect__Authority",
                () => identity.Resource.GetEndpoint("http").Url + "/software");
            return builder;
        }

        public IResourceBuilder<ProjectResource> WithIdentityOpenIdBearer(IResourceBuilder<ContainerResource> identity)
        {
            builder.WithEnvironment("Authentication__Schemes__Bearer__Authority",
                () => identity.Resource.GetEndpoint("http").Url + "/software");
            return builder;
        }
    }

    extension(IResourceBuilder<ContainerResource> builder)
    {
        public IResourceBuilder<ContainerResource> WithGetTokenForMary()
        {
            builder.WithHttpCommand("/software/token", "Get Mary (Manager) Token", commandName: "Mary",
                commandOptions: new HttpCommandOptions
                {
                    Description = "Get Mary (Manager) Token",
                    GetCommandResult = context =>
                    {
                        Console.WriteLine("Getting Token for Mary");
                        return OnGetResultForCommand(context);
                    },
                    Method = HttpMethod.Post,
                    PrepareRequest = context =>
                    {
                        ModifyRequest(context.Request, "mary");

                        return Task.CompletedTask;
                    }
                });
            return builder;
        }

        public IResourceBuilder<ContainerResource> WithGetTokenForEarl()
        {
            builder.WithHttpCommand("/software/token", "Get Earl (SoftwareCenter) Token", commandName: "Earl",
                commandOptions: new HttpCommandOptions
                {
                    Description = "Get Earl (SoftwareCenter) Token",
                    GetCommandResult = context =>
                    {
                        Console.WriteLine("Getting Token for Earl");
                        return OnGetResultForCommand(context);
                    },
                    Method = HttpMethod.Post,
                    PrepareRequest = context =>
                    {
                        ModifyRequest(context.Request, "earl");

                        return Task.CompletedTask;
                    }
                });
            return builder;
        }

        public IResourceBuilder<ContainerResource> WithGetTokenForAlice()
        {
            builder.WithHttpCommand("/software/token", "Get Alice (Employee) Token", commandName: "Alice",
                commandOptions: new HttpCommandOptions
                {
                    Description = "Get Alice (Employee) Token",
                    GetCommandResult = context =>
                    {
                        Console.WriteLine("Getting Token for Alice");
                        return OnGetResultForCommand(context);
                    },
                    Method = HttpMethod.Post,
                    PrepareRequest = context =>
                    {
                        ModifyRequest(context.Request, "alice");

                        return Task.CompletedTask;
                    }
                });
            return builder;
        }
    }
}

public record AccessTokenResult
{
    [JsonPropertyName("access_token")] public string AccessToken { get; init; } = string.Empty;
}