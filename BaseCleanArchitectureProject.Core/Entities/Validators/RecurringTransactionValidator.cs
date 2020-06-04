using System;
using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class RecurringTransactionValidator : AbstractValidator<RecurringTransaction> {
		public RecurringTransactionValidator() {
			RuleFor(c => c.Name).NotEmpty().WithMessage("Inform a name for this Recurring Transaction");
			RuleFor(c => c.StartDate).NotNull().WithMessage("Inform the start date of this Recurring Transaction");
			RuleFor(c => c.Value).GreaterThan(0).WithMessage("Inform the value of this Recurring Transaction");
			RuleFor(c => c.Currency).NotNull().WithMessage("Inform the Currency of this Recurring Transaction");
		}
		
	}

}