using FluentValidation;
using SmartGallery.Shared;

namespace SmartGallery.Client.Validators
{
	public class RegisterViewModelValidator:AbstractValidator<RegisterViewModel>
	{
        const int maxLength = 500;
        public RegisterViewModelValidator()
		{
			RuleFor(e => e.Address).NotEmpty().WithMessage("Please Enter Your Address");
			RuleFor(e => e.Address).MaximumLength(maxLength).WithMessage("Address Field Can't Exceed 500 letters");
			RuleFor(e => e.Email).EmailAddress().WithMessage("You must Enter a Valid Email");
			RuleFor(e => e.ConfirmPassword).Equal(e => e.Password).WithMessage("Confirm Password Must Equal Password");
			RuleFor(e => e.Password).NotEmpty().WithMessage("Please Enter your Password");
			RuleFor(e => e.Password).MinimumLength(8).WithMessage("Your Password Must Conatain Atleast 8 characters");
			RuleFor(p => p.Password).Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.");
			RuleFor(p => p.Password).Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");
			RuleFor(p => p.Password).Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
			RuleFor(x => x.Password).Must(password => password!.Any(c => !char.IsLetterOrDigit(c)));

        }
    }
}

