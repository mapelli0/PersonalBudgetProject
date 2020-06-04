using System;
using System.Linq;
using System.Threading;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Infrastructure.Data;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class AccountFactory : BaseFactory<Account> {
		private readonly BaseCleanArchitectureProjectDbContext _dbContext;

		public AccountFactory() {
			
		}


		public AccountFactory (BaseCleanArchitectureProjectDbContext dbContext) : this() {
			_dbContext = dbContext;
		}

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
			this.WithName("Test Account").WithNumber("1234").WithBankName("Test Bank").WithCurrency("GBP");
			return this;
		}

		public AccountFactory WithCurrency (string iso = "") {
			if((_dbContext != null) && (!String.IsNullOrEmpty(iso))) {
				var currency = _dbContext.Set<Currency>().FirstOrDefault(c => c.ISO == iso);
				if (currency != null) {
					this.Entity.Currency = currency;
					this.Entity.CurrencyId = currency.Id;
				}
			}
			return this;
		}

	};

}