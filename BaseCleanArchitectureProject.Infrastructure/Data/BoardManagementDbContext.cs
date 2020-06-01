using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.EFCore.Extensions;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.Infrastructure.Data {

	public class BaseCleanArchitectureProjectDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid> {
		private readonly IMediator _dispatcher;

		public BaseCleanArchitectureProjectDbContext (DbContextOptions<BaseCleanArchitectureProjectDbContext> options, IMediator dispatcher): base(options) {
			_dispatcher = dispatcher;
		}

		protected override void OnModelCreating (ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

			// alternately this is built-in to EF Core 2.2
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) {
			var changedEntities = ChangeTracker.Entries<IBaseEntity>().Where(e => e.State == EntityState.Modified).Select(e => e.Entity);
			foreach (var changedEntity in changedEntities) {
				changedEntity.Update();
			}

			int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			// ignore events if no dispatcher provided
			if (_dispatcher == null) {
				return result;
			}

			// dispatch events only if save was successful
			var entitiesWithEvents = ChangeTracker.Entries<IBaseEntity>().Select(e => e.Entity).Where(e => e.Events.Any()).ToArray();
			foreach (var entity in entitiesWithEvents) {
				var events = entity.Events.ToArray();
				entity.Events.Clear();
				foreach (var domainEvent in events) {
					await _dispatcher.Send(domainEvent, cancellationToken).ConfigureAwait(false);
				}
			}
			return result;
		}

		public override int SaveChanges() {
			return SaveChangesAsync().GetAwaiter().GetResult();
		}
	}

}