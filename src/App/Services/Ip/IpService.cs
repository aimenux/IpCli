using System.Net.NetworkInformation;
using App.Extensions;

namespace App.Services.Ip;

public class IpService : IIpService
{
    private readonly HttpClient _httpClient;

    public IpService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public Task<ICollection<PrivateIp>> GetPrivateIpsAsync(IpParameters parameters, CancellationToken cancellationToken)
    {
        var privateIps = new List<PrivateIp>();
        
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            var networkName = networkInterface.Name;
            var networkType = networkInterface.NetworkInterfaceType.ToString();
            var networkStatus = networkInterface.OperationalStatus.ToString();

            var ipProperties = networkInterface.GetIPProperties();
            var ipAddresses = ipProperties.UnicastAddresses;

            var ipv4 = ipAddresses.Count >= 2
                ? ipAddresses[1].Address.ToString()
                : null;
            
            var ipv6 = ipAddresses.Count >= 1
                ? ipAddresses[0].Address.ToString()
                : null;

            var privateIp = new PrivateIp
            {
                IpV4 = ipv4,
                IpV6 = ipv6,
                NetworkName = networkName,
                NetworkType = networkType,
                NetworkStatus = networkStatus
            };
            
            privateIps.Add(privateIp);
        }

        return Task.FromResult<ICollection<PrivateIp>>(privateIps);
    }

    public async Task<ICollection<PublicIp>> GetPublicIpsAsync(IpParameters parameters, CancellationToken cancellationToken)
    {
        var tasks = parameters.SourceUrls.Select(url => GetPublicIpAsync(url, cancellationToken));
        var publicIps = await Task.WhenAll(tasks);
        return publicIps;
    }

    private async Task<PublicIp> GetPublicIpAsync(string sourceUrl, CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync(sourceUrl, cancellationToken);
        var sourceResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new PublicIp
            {
                IpV4 = null,
                SourceUrl = sourceUrl,
                SourceResponse = sourceResponse
            };
        }
        
        var ipV4 = sourceResponse.Trim();
        if (!ipV4.IsValidIpV4())
        {
            return new PublicIp
            {
                IpV4 = null,
                SourceUrl = sourceUrl,
                SourceResponse = sourceResponse
            };            
        }
        
        return new PublicIp
        {
            IpV4 = ipV4,
            SourceUrl = sourceUrl,
            SourceResponse = sourceResponse
        };
    }
}