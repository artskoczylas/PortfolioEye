using System.Net;
using System.Text.Json;
using FluentAssertions;
using PortfolioEye.Application;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Tests.Extensions;

public class HttpClientFactoryExtensionsTests
{
    private record TestDataRecord(string Abc);

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
    public async Task ToResult_WithSuccessfulResponse_ReturnsValidData()
    {
        var response = await PrepareSuccessResponse(new TestDataRecord("Valid data"));

        var result = await response.ToResult<TestDataRecord>();

        result.Data.Should().NotBeNull();
        result.Data!.Abc.Should().Be("Valid data");
    }

    [Test]
    public async Task ToResult_WithFailedResponse_ReturnsValidFlag()
    {
        var response = await PrepareFailResponse(HttpStatusCode.NotFound,"Some error");

        var result = await response.ToResult<TestDataRecord>();

        result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task ToResult_WithFailedResponse_ReturnsValidMessage()
    {
        var response = await PrepareFailResponse(HttpStatusCode.NotFound,"Some error");

        var result = await response.ToResult<TestDataRecord>();

        result.Messages.Should().HaveCount(1);
        result.Messages.First().Should().Be("Some error");
    }

    [Test]
    public async Task ToResult_WithFailedResponse_ReturnsValidErrorCode()
    {
        var response = await PrepareFailResponse(HttpStatusCode.NotFound,"Some error");

        var result = await response.ToResult<TestDataRecord>();

        result.ErrorCode.Should().Be(WellKnown.ErrorCodes.NotFound);
    }

    [Test]
    public async Task ToResult_WithFailedResponse_ReturnsEmptyData()
    {
        var response = await PrepareFailResponse(HttpStatusCode.NotFound,"Some error");

        var result = await response.ToResult<TestDataRecord>();

        result.Data.Should().BeNull();
    }

    private async Task<HttpResponseMessage> PrepareSuccessResponse(TestDataRecord record)
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(await Result<TestDataRecord>.SuccessAsync(record)))
        };
    }

    private async Task<HttpResponseMessage> PrepareFailResponse(HttpStatusCode code, string message)
    {
        return new HttpResponseMessage(code)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(await Result<TestDataRecord>.FailAsync((int)code, message)))
        };
    }
}