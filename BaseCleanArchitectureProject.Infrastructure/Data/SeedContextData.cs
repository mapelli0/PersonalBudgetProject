using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BaseCleanArchitectureProject.Infrastructure.Data {

	public static class SeedContextData {
		public static async Task SeedData (this BaseCleanArchitectureProjectDbContext dbContext, CancellationToken cancellationToken) {
			await SeedRolesAsync(dbContext, cancellationToken);
			await SeedAdministratorUserAsync(dbContext, cancellationToken);
			await SeedCurrencyAsync(dbContext, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);
		}

		private static async Task SeedCurrencyAsync (BaseCleanArchitectureProjectDbContext dbContext, CancellationToken cancellationToken) {
			var real = new Currency() {
											Name = "Real",
											Country = "Brazil",
											Symbol = "R$",
											ISO = "BRL"
									};
			var pound = new Currency() {
												Name = "British Pound",
												Country = "UK",
												Symbol = "£",
												ISO = "GBP"
										};
			var euro = new Currency() {
											Name = "Euro",
											Country = "EU",
											Symbol = "€",
											ISO = "EUR"
									};
			await CreateCurrencyIfNotExistAsync(dbContext, real, cancellationToken);
			await CreateCurrencyIfNotExistAsync(dbContext, pound, cancellationToken);
			await CreateCurrencyIfNotExistAsync(dbContext, euro, cancellationToken);
		}

		private static async Task CreateCurrencyIfNotExistAsync (BaseCleanArchitectureProjectDbContext dbContext, Currency currency, CancellationToken cancellationToken) {
			var exist = dbContext.Set<Currency>().FirstOrDefault(c => c.ISO == currency.ISO);
			if (exist == null) {
				await dbContext.Set<Currency>().AddAsync(currency, cancellationToken);
			}
		}

		private static async Task SeedAdministratorUserAsync (BaseCleanArchitectureProjectDbContext dbContext, CancellationToken cancellationToken) {
			var userStore = new UserStore<ApplicationUser, ApplicationRole, BaseCleanArchitectureProjectDbContext, Guid>(dbContext);
			using (var um = new UserManager<ApplicationUser>(userStore, null, new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null)) {
				await CreateUserIfNotExistsAsync(um, "Admin", Role.ADMIN, cancellationToken).ConfigureAwait(false);
			}
		}

		private static async Task CreateUserIfNotExistsAsync (UserManager<ApplicationUser> um, string userName, string role, CancellationToken cancellationToken) {
			var existingUser = await um.FindByNameAsync(userName).ConfigureAwait(false);
			if (existingUser == null) {
				var user = new ApplicationUser(userName);
				await um.CreateAsync(user, "aq112233").ConfigureAwait(false);
				await um.AddToRoleAsync(user, role).ConfigureAwait(false);
			}
		}

		private static async Task SeedRolesAsync (BaseCleanArchitectureProjectDbContext dbContext, CancellationToken cancellationToken) {
			var roleS = new RoleStore<ApplicationRole, BaseCleanArchitectureProjectDbContext, Guid>(dbContext);
			using (var rm = new RoleManager<ApplicationRole>(roleS, null, null, null, null)) {
				await AddRoleIfNotExist(Role.ADMIN, rm);
				await AddRoleIfNotExist(Role.User, rm);
			}
		}

		private static async Task AddRoleIfNotExist (string role, RoleManager<ApplicationRole> rm) {
			var existingRole = await rm.FindByNameAsync(role).ConfigureAwait(false);
			if (existingRole == null) {
				await rm.CreateAsync(new ApplicationRole(role)).ConfigureAwait(false);
			}
		}
	}

}