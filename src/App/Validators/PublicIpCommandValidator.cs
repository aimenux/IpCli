using App.Commands;
using FluentValidation;

namespace App.Validators;

public class PublicIpCommandValidator : AbstractValidator<PublicIpCommand>
{
    public PublicIpCommandValidator()
    {
    }
}