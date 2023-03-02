using App.Commands;
using FluentValidation;

namespace App.Validators;

public static class ToolCommandValidator
{
    public static ValidationErrors Validate<TCommand>(TCommand command) where TCommand : AbstractCommand
    {
        return command switch
        {
            ToolCommand _ => ValidationErrors.New<ToolCommand>(),
            PublicIpCommand publicIpCommand => Validate(new PublicIpCommandValidator(), publicIpCommand),
            PrivateIpCommand privateIpCommand => Validate(new PrivateIpCommandValidator(), privateIpCommand),
            _ => throw new ArgumentOutOfRangeException(nameof(command), typeof(TCommand), "Unexpected command type")
        };
    }

    private static ValidationErrors Validate<TCommand>(IValidator<TCommand> validator, TCommand command) where TCommand : AbstractCommand
    {
        var errors = validator
            .Validate(command)
            .Errors;
        return ValidationErrors.New<TCommand>(errors);
    }
}