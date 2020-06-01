using FluentValidation;
using FluentValidation.AspNetCore;

namespace BaseCleanArchitectureProject.Core.Entities.Address {

	public class AddressValidator : AbstractValidator<Address> {
		public AddressValidator() {
			RuleFor(c => c.Postcode).NotNull().NotEmpty().WithMessage("Please inform a valid Postcode address");
			RuleFor(a => a.Address1).NotNull().NotEmpty().WithMessage("Please inform a valid Address");
			RuleFor(a => a.City).NotNull().NotEmpty().WithMessage("Please inform a valid City");
		}
	}

}