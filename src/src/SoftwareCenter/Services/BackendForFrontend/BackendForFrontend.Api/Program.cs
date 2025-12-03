using BackendForFrontend.Api.Endpoints.BffUser;
using BackendForFrontend.Api.Extensions.Auth;
using BackendForFrontend.Api.Extensions.OpenApi;
using BackendForFrontend.Api.Extensions.Yarp;
using Duende.AccessTokenManagement.OpenIdConnect;
using SoftwareCenter.Core;

var builder = WebApplication.CreateBuilder(args);

// I do this all the time. Take the header off the response that says what web server you are using.

builder.WebHost.ConfigureKestrel(options => { options.AddServerHeader = false; });
builder.AddServiceDefaults();
builder.Services.AddSoftwareCenterOpenApiWithTransforms<BffOpenApiTransform>("v1");

builder.AddSoftwareCenterCorsForOpenApiDuringDevelopment();

builder.AddBffYarpReverseProxy();

builder.AddAuthenticationSchemes();

builder.Services.AddOpenIdConnectAccessTokenManagement();


// just this week angular had a big patch because they handled this wrong.
// UPDATE YOUR ANGULAR APPS
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "__SoftwareCenterBFF-X-XSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddProblemDetails();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages();
app.UseExceptionHandler();
app.UseAntiforgery();

app.MapBffUserApi();



app.MapReverseProxy();
app.MapSoftwareCenterOpenApiDuringDevelopment();

app.MapDefaultEndpoints();


app.Run();