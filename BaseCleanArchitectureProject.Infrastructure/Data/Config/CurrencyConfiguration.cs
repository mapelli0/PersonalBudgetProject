using System;
using BaseCleanArchitectureProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public class CurrencyConfiguration : BaseConfiguration<Currency, Guid> {
		public override void Configure (EntityTypeBuilder<Currency> builder) {
			base.Configure(builder);
		}
	}

}