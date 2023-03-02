using App.Configuration;
using App.Services.Console;
using App.Services.Ip;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;

namespace App.Commands;

[Command("Private", FullName = "Get private ip", Description = "Get private ip.")]
public class PrivateIpCommand : AbstractCommand
{
    private readonly IIpService _ipService;
    private readonly IOptions<Settings> _options;

    public PrivateIpCommand(IIpService ipService, IConsoleService consoleService, IOptions<Settings> options) : base(consoleService)
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
            var privateIps = await _ipService.GetPrivateIpsAsync(parameters, cancellationToken);
            ConsoleService.RenderPrivateIps(privateIps);
        });
    }
}