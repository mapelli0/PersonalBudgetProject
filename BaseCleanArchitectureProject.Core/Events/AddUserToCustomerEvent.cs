using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Customer;
using BaseCleanArchitectureProject.Core.Entities.Identity;

namespace BaseCleanArchitectureProject.Core.Events {

	public class AddUserToCustomerEvent : BaseDomainEvent {
		public string InitialPassword { get; private set; }

		public Customer Customer { get; private set; }

		public ApplicationUser User { get; private set; }
		
		
		public AddUserToCustomerEvent(Customer customer, ApplicationUser user, string initialPassword) {
			InitialPassword = initialPassword;
			Customer = customer;
			User = user;
		}

		


	}

}