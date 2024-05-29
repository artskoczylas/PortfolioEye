using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Tests.Services;

[TestFixture]
[TestOf(typeof(BondInformationsReader))]
public class BondInformationsReaderTest
{

    [Test]
    public void Testing()
    {
        var reader = new BondInformationsReader();
        reader.ReadInformation();
        
        
        Assert.Pass();
    }
}