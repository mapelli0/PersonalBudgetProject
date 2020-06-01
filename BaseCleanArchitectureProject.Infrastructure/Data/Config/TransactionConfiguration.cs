using System;
using BaseCleanArchitectureProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public class TransactionConfiguration : BaseConfiguration<Transaction, Guid> {
		public override void Configure (EntityTypeBuilder<Transaction> builder) {
			base.Configure(builder);
		}
	}

}