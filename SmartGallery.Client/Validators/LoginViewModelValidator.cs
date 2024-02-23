using FluentValidation;
using SmartGallery.Shared;

namespace SmartGallery.Client.Validators;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
	public LoginViewModelValidator()
	{
        RuleFor(e => e.Email).NotEmpty().WithMessage("Please Enter Your Email");
        RuleFor(e => e.Email).EmailAddress().WithMessage("You must Enter a Valid Email");
        RuleFor(e => e.Password).NotEmpty().WithMessage("Please Enter Your Password");
    }
}

