using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Tests.Services;

[TestFixture]
[TestOf(typeof(BondInformationsReader))]
public class BondInformationsReaderTest
{

    [Test]
    public void Testing()
    {
        var provider = new BondInformationProvider();
        
        var reader = new BondInformationsReader();
        var result = reader.ReadInformation(provider.GetCurrentBondInformation());
        
        
        Assert.Pass();
    }
}