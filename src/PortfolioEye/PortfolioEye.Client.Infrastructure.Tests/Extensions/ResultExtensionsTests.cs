using System.Net;
using System.Text.Json;
using eastsoft.RCP.Shared.Wrappers;
using FluentAssertions;
using PortfolioEye.Client.Infrastructure.Extensions;
namespace PortfolioEye.Infrastructure.Tests.Extensions;



public class HttpClientFactoryExtensionsTests
{
    private record TestDataRecord(string Abc);
    [SetUp]
    public void Setup()
    {
        
    }
    [Test]
    public async Task ToResult_WithSuccessfulResponse_ReturnsValidFlag()
    {
        var response = await PrepareSuccessResponse(new TestDataRecord("Valid data"));
        
        var result = await response.ToResult<TestDataRecord>();
        
        result.IsSuccess.Should().BeTrue();
    }
    [Test]
    public async Task ToResult_WithSuccessfulResponse_ReturnsEmptyMessages()
    {
        var response = await PrepareSuccessResponse(new TestDataRecord("Valid data"));
        
        var result = await response.ToResult<TestDataRecord>();
        
        result.Messages.Should().NotBeNull();
        result.Messages.Should().BeEmpty();
    }
    [Test]
    public async Task ToResult_WithSuccessfulResponse_ReturnsValidResult()
    {
        var response = await PrepareSuccessResponse(new TestDataRecord("Valid data"));
        
        var result = await response.ToResult<TestDataRecord>();
        
        result.Data.Should().NotBeNull();
        result.Data!.Abc.Should().Be("Valid data");
    }
    
    private async Task<HttpResponseMessage> PrepareSuccessResponse(TestDataRecord record)
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(await Result<TestDataRecord>.SuccessAsync(record)))
        };
    }
}