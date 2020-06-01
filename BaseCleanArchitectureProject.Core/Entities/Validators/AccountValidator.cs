using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class AccountValidator : AbstractValidator<Account> {
		public AccountValidator() {
			RuleFor(a => a.Name).NotEmpty().NotNull();
			RuleFor(a => a.Number).NotEmpty().NotNull();
			RuleFor(a => a.Bank).NotNull();
		}
		
	}

}