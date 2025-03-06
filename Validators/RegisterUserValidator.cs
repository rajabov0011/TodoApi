using FluentValidation;
using TodoApi.Dtos;

namespace TodoApi.Validators;

public class RegisterUserValidator : AbstractValidator<CreateUser>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}