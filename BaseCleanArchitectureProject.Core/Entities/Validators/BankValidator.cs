using BaseCleanArchitectureProject.Core.Entities.Institution;
using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class BankValidator : AbstractValidator<Bank> {
		public BankValidator() {
			RuleFor(b => b.Name).NotEmpty().NotNull();
		}
		
	}

}