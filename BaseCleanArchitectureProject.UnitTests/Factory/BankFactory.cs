using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class BankFactory: BaseFactory<Bank> {
		public BankFactory WithName (string name = "") {
			Entity.Name = name;
			return this;
		}
	}

}