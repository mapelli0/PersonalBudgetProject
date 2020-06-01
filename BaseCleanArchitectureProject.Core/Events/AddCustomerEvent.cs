using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Customer;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using MediatR;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.Core.Events {

	public class AddCustomerEvent : BaseDomainEvent {
		public Customer AddedCustomer { get; private set; }

		public ApplicationUser User { get; private set; }

		public AddCustomerEvent (Customer addedCustomer, ApplicationUser user) {
			AddedCustomer = addedCustomer;
			User = user;
		}
	}

}