
using Vendors.Api.Tests.Fixtures;
using Xunit;

namespace Vendors.Api.Tests;

[Collection("VendorsCollection")]

public class AddingAVendor(VendorsBaseFixture fixture)
{

    [Fact]
    public async Task LetsSee()
    {
        Assert.True(true);
    }
    
}