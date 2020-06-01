using System;
using BaseCleanArchitectureProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public class AccountConfiguration : BaseConfiguration<Account,Guid> {
		public override void Configure (EntityTypeBuilder<Account> builder) {
			base.Configure(builder);
			AddNavigationProperty(builder, nameof(Account.Transactions));
		}
	}

}