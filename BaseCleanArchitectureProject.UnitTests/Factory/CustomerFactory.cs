using BaseCleanArchitectureProject.Core.Entities.Customer;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class CustomerFactory: BaseFactory<Customer> {
		public CustomerFactory WithName (string name) {
			Entity.CompanyName = name;
			return this;
		}

		public CustomerFactory WithEmail (string email) {
			Entity.Email = email;
			return this;
		}

		public CustomerFactory WithPassword (string pass) {
			Entity.InitialPassword = pass;
			return this;
		}

		public CustomerFactory WithDefaultValues() {
			return WithName("Customer Test").WithEmail("alfredo.mapelli@gmail.com").WithPassword("Aq112233").WithContactName("Alfredo Mapelli").WithAddress();
		}

		public CustomerFactory WithContactName(string contactName) {
			Entity.ContactName = contactName;
			return this;
		}



		public CustomerFactory WithMissingAddressInformation() {
			var add = new AddressFactory().WithPostCode("GU12 4DS").Build();
			Entity.Address = add;
			return this;
		}

		public CustomerFactory WithAddress() {
			var add = new AddressFactory().WithDefaultValues().Build();
			Entity.Address = add;
			return this;
		}
	}

}