using App.Commands;
using App.Services.Console;
using App.Validators;
using FluentAssertions;
using FluentValidation.Results;
using McMaster.Extensions.CommandLineUtils;

namespace Tests.Validators;

public class ValidationErrorTests
{
    [Fact]
    public void Should_Get_OptionName()
    {
        // arrange
        var validationFailure = new ValidationFailure(nameof(FakeCommand.TurboMode), "Required option");
        var validationError = ValidationError.New<FakeCommand>(validationFailure);

        // act
        var optionName = validationError.OptionName();

        // assert
        optionName.Should().NotBeNullOrWhiteSpace();
        optionName.Should().Be("-t|--turbo");
    }

    private sealed class FakeCommand : AbstractCommand
    {
        public FakeCommand(IConsoleService consoleService) : base(consoleService)
        {
        }
        
        [Option("-t|--turbo", "Turbo mode", CommandOptionType.SingleValue)]
        public string TurboMode { get; init; }

        protected override Task ExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}