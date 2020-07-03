using System;
using BaseCleanArchitectureProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public class RecurringTransactionConfiguration : BaseConfiguration<RecurringTransaction, Guid> {
		public override void Configure (EntityTypeBuilder<RecurringTransaction> builder) {
			base.Configure(builder);
			builder.HasMany(rt => rt.RecordedTransactions).WithOne(rt => rt.RecurringTransaction).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
			AddNavigationProperty(builder, nameof(RecurringTransaction.RecordedTransactions));
		}
	}

}