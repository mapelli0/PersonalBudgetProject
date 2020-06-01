using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities.Identity {

	public class ApplicationUser : IdentityUser<Guid> {
		public ApplicationUser() {
			this.Id = Guid.NewGuid();
		}
		public ApplicationUser(string userName): this() {
			this.UserName = userName;
		}

		[NotMapped]
		public string InitialPassword { get; set; }

	}

}