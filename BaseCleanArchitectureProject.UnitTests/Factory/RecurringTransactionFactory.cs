using System;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class RecurringTransactionFactory: BaseFactory<RecurringTransaction> {
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

		public RecurringTransactionFactory WithBillDefaultValues() {
			return WithValue().WithRecurringTransactionType(TransactionType.Debit).WithIsAchived().WithName("Gas bill").WithStartDate(DateTime.Today).WithPeriodicity();
		}

		public RecurringTransactionFactory WithCreditDefaultValues() {
			return WithValue().WithRecurringTransactionType(TransactionType.Credit).WithIsAchived().WithName("Salary").WithStartDate(DateTime.Today).WithPeriodicity(Periodicity.Monthly);
		}
	}

}