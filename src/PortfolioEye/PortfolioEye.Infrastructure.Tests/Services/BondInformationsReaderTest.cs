using FluentAssertions;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Tests.Services;

[TestFixture]
[TestOf(typeof(BondInformationsReader))]
public class BondInformationsReaderTest
{
    Stream sampleFileStream;
    BondInformationsReader reader;
    [SetUp]
    public void SetUp()
    {
        var sampleFile = Resources.Files.Dane_dotyczace_obligacji_detalicznych;
        sampleFileStream = new MemoryStream(sampleFile);
        reader = new BondInformationsReader();
    }

    [Test]
    public void EDO1014Series_Exists()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO1014");

        testSeries.Should().NotBeNull();
    }

    [Test]
    public void EDO1014Series_HaveValidData()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO1014");

        testSeries.Isin.Should().Be("PL0000103594");
        testSeries.SaleStart.Should().Be(new DateOnly(2004, 10, 01));
        testSeries.SaleEnd.Should().Be(new DateOnly(2004, 10, 31));
        testSeries.Price.Should().Be(100m);
        testSeries.ConvertPrice.Should().BeNull();
        testSeries.InterestPln.Should().Be(84.08m);
        testSeries.Margin.Should().Be(0.035m);
    }

    [Test]
    public void EDO1014Series_HaveValidFirstYearRate()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO1014");
        var firstYear = testSeries.Years.FirstOrDefault(x => x.YearNo == 1);
        firstYear.Should().NotBeNull();
        firstYear!.InterestRate.Should().Be(0.071m);
    }

    [Test]
    public void EDO1014Series_HaveValidLastYearRate()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO1014");
        var firstYear = testSeries.Years.FirstOrDefault(x => x.YearNo == 10);
        firstYear.Should().NotBeNull();
        firstYear!.InterestRate.Should().Be(0.046m);
    }

    [Test]
    public void EDO0634Series_Exists()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO0634");

        testSeries.Should().NotBeNull();
    }
    
    [Test]
    public void EDO0634Series_HaveValidData()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO0634");

        testSeries.Isin.Should().Be("PL0000116992");
        testSeries.SaleStart.Should().Be(new DateOnly(2024, 06, 01));
        testSeries.SaleEnd.Should().Be(new DateOnly(2024, 06, 30));
        testSeries.Price.Should().Be(100m);
        testSeries.ConvertPrice.Should().Be(99.6m);
        testSeries.InterestPln.Should().BeNull();
        testSeries.Margin.Should().Be(0.02m);
    }

    [Test]
    public void EDO0634Series_HaveValidFirstYearRate()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO0634");
        var firstYear = testSeries.Years.FirstOrDefault(x => x.YearNo == 1);
        firstYear.Should().NotBeNull();
        firstYear!.InterestRate.Should().Be(0.068m);
    }

    [Test]
    public void EDO0634Series_HaveValidLastYearRate()
    {
        var result = reader.ReadInformation(sampleFileStream);

        var testSeries = result.FirstOrDefault(x => x.Series == "EDO0634");
        var firstYear = testSeries.Years.FirstOrDefault(x => x.YearNo == 10);
        firstYear.Should().NotBeNull();
        firstYear!.InterestRate.Should().BeNull();
    }
}