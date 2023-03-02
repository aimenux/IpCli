using App.Configuration;
using App.Services.Console;
using App.Services.Ip;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;

namespace App.Commands;

[Command("Public", FullName = "Get public ip", Description = "Get public ip.")]
public class PublicIpCommand : AbstractCommand
{
    private readonly IIpService _ipService;
    private readonly IOptions<Settings> _options;

    public PublicIpCommand(IIpService ipService, IConsoleService consoleService, IOptions<Settings> options) : base(consoleService)
    {
        _ipService = ipService ?? throw new ArgumentNullException(nameof(ipService));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task ExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
    {
        var parameters = new IpParameters
        {
            SourceUrls = _options.Value.SourceUrls
        };

        await ConsoleService.RenderStatusAsync(async () =>
        {
            var publicIps = await _ipService.GetPublicIpsAsync(parameters, cancellationToken);
            ConsoleService.RenderPublicIps(publicIps);
        });
    }
}