using Alba;
using Alba.Security;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Testcontainers.PostgreSql;
using Wolverine;

namespace Vendors.Api.Tests.Fixtures;

public class VendorsBaseFixture : IAsyncLifetime
{
    private PostgreSqlContainer postgres = null!;
    public IAlbaHost Host { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        postgres = new PostgreSqlBuilder()
            .WithImage("postgres:17.5-bullseye")
            .WithDatabase("vendors")
            .WithReuse(true)
            .Build();
        await postgres.StartAsync(TestContext.Current.CancellationToken);

        Host = await AlbaHost.For<Program>(config =>
        {
            config.UseSetting("ConnectionStrings:vendors", postgres.GetConnectionString());
            config.ConfigureTestServices(services =>
            {
                services.RunWolverineInSoloMode();
                services.DisableAllExternalWolverineTransports();
            });
        }, new AuthenticationStub());
    }
    public async ValueTask DisposeAsync()
    {
        await Host.DisposeAsync();
        await postgres.DisposeAsync();
    }
}

[CollectionDefinition("VendorsCollection")]
public class StubbedAuthenticationHost : ICollectionFixture<VendorsBaseFixture>;