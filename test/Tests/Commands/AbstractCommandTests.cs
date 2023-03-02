using App.Commands;
using App.Configuration;
using App.Services.Console;
using App.Validators;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;

namespace Tests.Commands;

public class AbstractCommandTests
{
    [Fact]
    public async Task Should_Return_Ok()
    {
        // arrange
        var app = new CommandLineApplication();
        var service = new FakeConsoleService();
        var command = new FakeCommand(service)
        {
            Job = () => Task.CompletedTask
        };
        
        // act
        var result = await command.OnExecuteAsync(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Fact]
    public async Task Should_Return_Ko()
    {
        // arrange
        var app = new CommandLineApplication();
        var service = new FakeConsoleService();
        var command = new FakeCommand(service)
        {
            Job = () => throw new Exception("some error has occurred")
        };
        
        // act
        var result = await command.OnExecuteAsync(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
    
    private class FakeCommand : AbstractCommand
    {
        public Func<Task> Job { get; init; }

        public FakeCommand(IConsoleService consoleService) : base(consoleService)
        {
        }

        protected override async Task ExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
        {
            await Job.Invoke();
        }

        protected override bool HasValidOptionsAndArguments(out ValidationErrors validationErrors)
        {
            validationErrors = ValidationErrors.New<FakeCommand>();
            return true;
        }
    }
}