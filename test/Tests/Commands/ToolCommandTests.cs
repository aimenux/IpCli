using App.Commands;
using App.Configuration;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;

namespace Tests.Commands;

public class ToolCommandTests
{
    [Theory]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public async Task Should_ToolCommand_Return_Ok(bool showSettings, bool showVersion)
    {
        // arrange
        var app = new CommandLineApplication();
        var consoleService = new FakeConsoleService();
        var command = new ToolCommand(consoleService)
        {
            ShowSettings = showSettings,
            ShowVersion = showVersion
        };

        // act
        var result = await command.OnExecuteAsync(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
}