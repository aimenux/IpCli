using App.Services.Ip;
using FluentAssertions;

namespace Tests.Services;

public class IpServiceTests
{
    [Fact]
    public async Task Should_Get_PrivateIps()
    {
        // arrange
        var parameters = new IpParameters
        {
            SourceUrls = new List<string>
            {
                "https://ipinfo.io/ip"
            }
        };
        using var httpClient = new HttpClient();
        var ipService = new IpService(httpClient);

        // act
        var privateIps = await ipService.GetPrivateIpsAsync(parameters, CancellationToken.None);

        // assert
        privateIps.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task Should_Get_PublicIps()
    {
        // arrange
        var parameters = new IpParameters
        {
            SourceUrls = new List<string>
            {
                "https://ipinfo.io/ip"
            }
        };
        using var httpClient = new HttpClient();
        var ipService = new IpService(httpClient);

        // act
        var publicIps = await ipService.GetPublicIpsAsync(parameters, CancellationToken.None);

        // assert
        publicIps.Should().NotBeNullOrEmpty();
    }
}