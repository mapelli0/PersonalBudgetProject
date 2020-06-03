using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class AccountValidator : AbstractValidator<Account> {
		public AccountValidator() {
			RuleFor(a => a.Name).NotEmpty().WithMessage("Please inform the Account Name");
			RuleFor(a => a.Number).NotEmpty().WithMessage("Please inform the Account Number");
			RuleFor(a => a.Bank).NotEmpty().WithMessage("Please inform the Bank that this account Belong to");
			RuleFor(a => a.Bank).SetValidator(new BankValidator());
		}
		
	}

}