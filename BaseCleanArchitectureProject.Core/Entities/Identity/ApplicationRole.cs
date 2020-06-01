using System;
using Microsoft.AspNetCore.Identity;

namespace BaseCleanArchitectureProject.Core.Entities.Identity {

	public static class Role {
		public const string ADMIN = "Admin";
		public const string User = "User";
		
	}


	public class ApplicationRole : IdentityRole<Guid> {
		public ApplicationRole() {
			this.Id = Guid.NewGuid();
		}

		public ApplicationRole (string name) : this(){
			this.Name = name;
		}
	}

}