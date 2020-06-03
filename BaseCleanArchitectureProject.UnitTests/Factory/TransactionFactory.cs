using System;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.UnitTests.Builders;

namespace BaseCleanArchitectureProject.UnitTests.Factory {

	public class TransactionFactory : BaseFactory<Transaction> {

		public TransactionFactory WithName(string name = "") {
			Entity.Name = name;
			return this;
		}
		public TransactionFactory WithDate(DateTime? date = null) {
			if (date.HasValue) {
				Entity.Date = date.Value;
			}
			return this;
		}

		public TransactionFactory WithRandomValue() {
			Entity.Value = new Random().Next(0, 100000);
			return this;
		}

		public TransactionFactory WithDefaultValues() {
			return this.WithName("Transaction Test").WithDate(DateTime.Today).WithRandomValue();
		}

	}

}