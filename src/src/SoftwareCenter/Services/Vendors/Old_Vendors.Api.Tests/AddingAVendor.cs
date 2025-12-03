using NetTopologySuite.Utilities;
using Vendors.Api.Tests.Fixtures;
using Xunit;

namespace Vendors.Api.Tests;

[Collection("VendorsCollection")]
[SupportedOS(SupportedOS.macOS, SupportedOS.Windows)]
public class AddingAVendor(VendorsBaseFixture fixture)
{

    [Fact]
    public async Task LetsSee()
    {
        Assert.IsTrue(true);
    }
    
}