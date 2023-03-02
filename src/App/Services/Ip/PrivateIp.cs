namespace App.Services.Ip;

public class PrivateIp
{
    public string IpV4 { get; init; }
    public string IpV6 { get; init; }
    public string NetworkName { get; init; }
    public string NetworkType { get; init; }
    public string NetworkStatus { get; init; }
}