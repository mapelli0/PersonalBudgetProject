using System;
using System.Threading;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BaseCleanArchitectureProject.Infrastructure.Data {

	public static class SeedContextData {
		public static async Task SeedData (this BaseCleanArchitectureProjectDbContext dbContext, CancellationToken cancellationToken) {
			await SeedRolesAsync(dbContext, cancellationToken);
			await SeedAdministratorUser(dbContext, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);
		}

		private static async Task SeedAdministratorUser (BaseCleanArchitectureProjectDbContext dbContext, CancellationToken cancellationToken) {
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