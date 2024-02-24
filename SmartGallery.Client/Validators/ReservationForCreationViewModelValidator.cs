using System;
using FluentValidation;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Validators
{
	public class ReservationForCreationViewModelValidator : AbstractValidator<ReservationForCreationViewModel>
	{
		public ReservationForCreationViewModelValidator()
		{
			RuleFor(e => e.ProblemDescription).NotEmpty().WithMessage("Please Fill In Your Problem Description");
			RuleFor(e => e.ProblemDescription).MaximumLength(1000).WithMessage("Please Be More Specific Problem Description Can't Exceed 1000 characters");
			RuleFor(e => e.ReservationDate).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Please Enter Valid Date");
		}
	}
}

