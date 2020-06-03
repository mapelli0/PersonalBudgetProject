using System.Xml;
using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class TransactionValidator : AbstractValidator<Transaction> {
		public TransactionValidator() {
			RuleFor(t => t.Date).NotEmpty();
			RuleFor(t => t.Name).NotEmpty();
			RuleFor(t => t.Value).NotEmpty();
			RuleFor(t => t.Account).SetValidator(new AccountValidator());
		}
	}

}