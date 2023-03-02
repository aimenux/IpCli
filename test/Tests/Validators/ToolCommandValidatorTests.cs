using App.Commands;
using App.Configuration;
using App.Services.Console;
using App.Services.Ip;
using App.Validators;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Tests.Validators;

public class ToolCommandValidatorTests
{
    [Fact]
    public void Should_Get_ValidationErrors_For_ToolCommand()
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var command = new ToolCommand(consoleService);

        // act
        var validationErrors = ToolCommandValidator.Validate(command);

        // assert
        validationErrors.Should().NotBeNull();
    }
    
    [Fact]
    public void Should_Get_ValidationErrors_For_PublicIpCommand()
    {
        // arrange
        var ipService = Substitute.For<IIpService>();
        var consoleService = Substitute.For<IConsoleService>();
        var options = Options.Create(new Settings());
        var command = new PublicIpCommand(ipService, consoleService, options);

        // act
        var validationErrors = ToolCommandValidator.Validate(command);

        // assert
        validationErrors.Should().NotBeNull();
    }
    
    [Fact]
    public void Should_Get_ValidationErrors_For_PrivateIpCommand()
    {
        // arrange
        var ipService = Substitute.For<IIpService>();
        var consoleService = Substitute.For<IConsoleService>();
        var options = Options.Create(new Settings());
        var command = new PrivateIpCommand(ipService, consoleService, options);

        // act
        var validationErrors = ToolCommandValidator.Validate(command);

        // assert
        validationErrors.Should().NotBeNull();
    }
}