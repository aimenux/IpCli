using App.Commands;
using App.Configuration;
using App.Services.Ip;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;

namespace Tests.Commands;

public class PublicIpCommandTests
{
    [Fact]
    public async Task Should_PublicIpCommand_Return_Ok()
    {
        // arrange
        var app = new CommandLineApplication();
        using var httpClient = new HttpClient();
        var ipService = new IpService(httpClient);
        var consoleService = new FakeConsoleService();
        var options = Options.Create(new Settings());
        var command = new PublicIpCommand(ipService, consoleService, options);
        
        // act
        var result = await command.OnExecuteAsync(app);
        
        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
}