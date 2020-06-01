using System.Xml;
using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class TransactionValidator : AbstractValidator<Transaction> {
		public TransactionValidator() {
			RuleFor(t => t.Date).NotNull().NotEmpty();
			RuleFor(t => t.Name).NotNull().NotEmpty();
			RuleFor(t => t.Value).NotNull().NotEmpty();
		}
	}

}