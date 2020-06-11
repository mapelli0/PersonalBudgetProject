using FluentValidation;

namespace BaseCleanArchitectureProject.Core.Entities.Validators {

	public class CategoryValidator : AbstractValidator<Category> {
		public CategoryValidator() {
			RuleFor(b => b.Name).NotEmpty().WithMessage("Please inform the Bank name");
		}
		
	}

}