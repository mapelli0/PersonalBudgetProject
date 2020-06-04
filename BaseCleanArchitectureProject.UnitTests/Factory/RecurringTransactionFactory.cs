using System;
using System.Linq;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using BaseCleanArchitectureProject.Infrastructure.Data;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class RecurringTransactionFactory: BaseFactory<RecurringTransaction> {
		private readonly BaseCleanArchitectureProjectDbContext _dbContext;


		public RecurringTransactionFactory() {
			
		}

		public RecurringTransactionFactory(BaseCleanArchitectureProjectDbContext dbContext) : this() {
			_dbContext = dbContext;
		}


		public RecurringTransactionFactory WithRecurringTransactionType (TransactionType type) {
			Entity.RecurringTransactionType = type;
			return this;
		}

		public RecurringTransactionFactory WithName (string name = "") {
			Entity.Name = name;
			return this;
		}

		public RecurringTransactionFactory WithStartDate (DateTime? date = null) {
			if (date.HasValue) {
				Entity.StartDate = date.Value;
			}
			return this;
		}

		public RecurringTransactionFactory WithQuantity (int qty = 0) {
			Entity.Quantity = qty;
			return this;
		}

		public RecurringTransactionFactory WithPeriodicity (Periodicity periodicity = 0) {
			Entity.Periodicity = periodicity;
			return this;
		}

		public RecurringTransactionFactory WithIsAchived (bool isArchived = false) {
			Entity.IsArchived = isArchived;
			return this;
		}

		public RecurringTransactionFactory WithValue (double? value = null) {
			Entity.Value = value.GetValueOrDefault();
			if (!value.HasValue) {
				Entity.Value = new Random().Next(0, 3000);
			}
			return this;
		}

		public RecurringTransactionFactory WithCurrency (Currency currency) {
			this.Entity.Currency = currency;
			this.Entity.CurrencyId = currency.Id;
			return this;
		}


		public RecurringTransactionFactory WithCurrency (string iso = "") {
			if((_dbContext != null) && (!String.IsNullOrEmpty(iso))) {
				var currency = _dbContext.Set<Currency>().FirstOrDefault(c => c.ISO == iso);
				this.Entity.Currency = currency;
			}
			return this;
		}

		public RecurringTransactionFactory WithCreditDefaultValues() {
			return WithValue().WithRecurringTransactionType(TransactionType.Credit).WithIsAchived().WithName("Salary").WithStartDate(DateTime.Today).WithPeriodicity(Periodicity.Monthly);
		}

		public RecurringTransactionFactory WithBillDefaultValues() {
			return WithValue().WithRecurringTransactionType(TransactionType.Debit).WithIsAchived().WithName("Gas bill").WithStartDate(DateTime.Today).WithPeriodicity();
		}

	}

}