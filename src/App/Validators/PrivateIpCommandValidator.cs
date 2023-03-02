using App.Commands;
using FluentValidation;

namespace App.Validators;

public class PrivateIpCommandValidator : AbstractValidator<PrivateIpCommand>
{
    public PrivateIpCommandValidator()
    {
    }
}