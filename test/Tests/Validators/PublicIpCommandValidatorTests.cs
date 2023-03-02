using App.Commands;
using App.Configuration;
using App.Services.Console;
using App.Services.Ip;
using App.Validators;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Tests.Validators;

public class PublicIpCommandValidatorTests
{
    [Fact]
    public void PublicIpCommand_Should_Be_Valid()
    {
        // arrange
        var ipService = Substitute.For<IIpService>();
        var consoleService = Substitute.For<IConsoleService>();
        var options = Options.Create(new Settings());
        var command = new PublicIpCommand(ipService, consoleService, options);
        var validator = new PublicIpCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
}