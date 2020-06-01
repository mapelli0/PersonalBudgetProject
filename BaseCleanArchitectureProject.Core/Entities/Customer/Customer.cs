using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using BaseCleanArchitectureProject.Core.Events;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities.Customer {

	/// <summary>
	///     Customer
	///     To create a new customer user AddCustomer()
	/// </summary>
	public class Customer: BaseGuidEntity {
		private readonly ICollection<CustomerUsers> _users;
		public Customer() {
			_users = new HashSet<CustomerUsers>();
		}

		public IEnumerable<CustomerUsers> Users => _users.ToList().AsReadOnly();

		[Required]
		public string CompanyName { get; set; }

		[Required]
		public string ContactName { get; set; }

		public string Phone { get; set; }

		public string MobilePhone { get; set; }

		public Guid ApplicationUserId { get; set; }

		[Required]
		public ApplicationUser Administrator { get; set; }

		[Required]
		public string Email { get; set; }

		public Guid AddressId { get; set; }

		[Required]
		public Address.Address Address { get; set; }

		public bool FollowUpQuotes { get; set; }

		public bool PriorityCustomer { get; set; }


		[NotMapped]
		public string InitialPassword { get; set; }

		public static Customer AddCustomer (Customer customer, ApplicationUser user) {
			customer.Events.Add(new AddCustomerEvent(customer, user));
			return customer;
		}

		public void AddUser (ApplicationUser user, string initialPassword) {
			Events.Add(new AddUserToCustomerEvent(this, user, initialPassword));
			_users.Add(new CustomerUsers {
												Customer = this,
												User = user
										});
		}
	}

}