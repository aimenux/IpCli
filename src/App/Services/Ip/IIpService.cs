namespace App.Services.Ip;

public interface IIpService
{
    Task<ICollection<PrivateIp>> GetPrivateIpsAsync(IpParameters parameters, CancellationToken cancellationToken);
    Task<ICollection<PublicIp>> GetPublicIpsAsync(IpParameters parameters, CancellationToken cancellationToken);
}