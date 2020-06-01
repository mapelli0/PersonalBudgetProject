using BaseCleanArchitectureProject.Core.Entities.Address;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class AddressFactory : BaseFactory<Address> {
		public AddressFactory WithPostCode (string postCode) {
			Entity.Postcode = postCode;
			return this;
		}

		public AddressFactory WithAddress1 (string address1) {
			Entity.Address1 = address1;
			return this;
		}
		
		
		public AddressFactory WithCity (string city) {
			Entity.City = city;
			return this;
		}


		public AddressFactory WithDefaultValues() {
			this.WithAddress1("this is line 1 address").WithPostCode("XX12 4XX").WithCity("Test City");
			return this;
		}

	}

}