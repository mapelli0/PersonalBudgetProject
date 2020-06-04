using System.Data;
using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class BankValidator : AbstractValidator<Bank> {
		public BankValidator() {
			RuleFor(b => b.Name).NotEmpty().WithMessage("Please inform the Bank name");
		}
		
	}

}