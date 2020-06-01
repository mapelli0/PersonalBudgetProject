using System;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Institution;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public class BankConfiguration : BaseConfiguration<Bank, Guid> {
		public override void Configure (EntityTypeBuilder<Bank> builder) {
			base.Configure(builder);
			AddNavigationProperty(builder, nameof(Bank.Accounts));
		}
	}

}