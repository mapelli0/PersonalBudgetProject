using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Institution;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class AccountFactory : BaseFactory<Account> {
		public AccountFactory WithName (string name = "") {
			this.Entity.Name = name;
			return this;
		}
		public AccountFactory WithNumber (string number = "") {
			this.Entity.Number = number;
			return this;
		}

		public AccountFactory WithBank (Bank bank = null) {
			this.Entity.Bank = bank;
			return this;
		}

		public AccountFactory WithBankName (string bankName = "") {
			var bank = new BankFactory().WithName(bankName).Build();
			return WithBank(bank);
		}

		public AccountFactory WithInitialBalance (double initialValue = 0) {
			this.Entity.InitialBalance = initialValue;
			return this;
		}

		public AccountFactory WithDefaultValues() {
			this.WithName("Test Account").WithNumber("1234").WithBankName("Test Bank");
			return this;
		}
	};

}