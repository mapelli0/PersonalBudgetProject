using System;
using BaseCleanArchitectureProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public class CategoryConfiguration : BaseConfiguration<Category, Guid> {
		public override void Configure (EntityTypeBuilder<Category> builder) {
			base.Configure(builder);
			AddNavigationProperty(builder, nameof(Category.RecurringTransactions));
			AddNavigationProperty(builder, nameof(Category.Transactions));
			AddNavigationProperty(builder, nameof(Category.SubCategories));
		}
	}

}