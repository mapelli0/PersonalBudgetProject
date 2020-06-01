using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salftech.SharedKernel;
using Pluralize.NET.Core;

namespace BaseCleanArchitectureProject.Infrastructure.Data.Config {

	public abstract class BaseConfiguration<T, TKey>: IEntityTypeConfiguration<T> where T : BaseEntityId<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<T> builder) {
			var tableName = typeof(T).Name;
			var ps = new Pluralizer();
			builder.ToTable(ps.Pluralize(tableName));
			builder.HasKey(e => e.Id);
			builder.Ignore(e => e.Events);
		}


		protected void AddNavigationProperty (EntityTypeBuilder<T> builder, string propertyName) {
			var supplyNavigation = builder.Metadata.FindNavigation(propertyName);
			supplyNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}

}