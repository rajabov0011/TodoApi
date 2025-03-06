using FluentValidation;
using TodoApi.Dtos;

namespace TodoApi.Validators;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required");
    }
}