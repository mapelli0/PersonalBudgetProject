using System;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities.Customer {

	public class CustomerUsers : BaseEntityId<Guid> {

		public Guid CustomerId { get; set; }

		public Guid ApplicationUserId { get; set; }

		public Entities.Customer.Customer Customer { get; set; }

		public ApplicationUser User { get; set; }
		
	}

}