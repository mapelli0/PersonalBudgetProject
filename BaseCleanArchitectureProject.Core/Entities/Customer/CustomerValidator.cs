using BaseCleanArchitectureProject.Core.Entities.Address;
using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Customer {

	public class CustomerValidator : AbstractValidator<Customer> {
		public CustomerValidator() {
			//RuleFor(c => c.Administrator).NotNull().WithMessage("Administrator cannot be null");
			RuleFor(c => c.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Please inform a valid Email address");
			RuleFor(c => c.ContactName).NotNull().NotEmpty().WithMessage("Please inform a Contact Name");
			RuleFor(c => c.Address).SetValidator(new AddressValidator());
		}
	}

}