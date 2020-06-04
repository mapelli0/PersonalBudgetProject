using System;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.UnitTests.Factory;
using FluentAssertions;
using Xunit;

namespace BaseCleanArchitectureProject.UnitTests {

	public class RecurringTransactionTest {
		[Fact]
		public void RecurringTransactionWithQuantityOneShouldHaveOneFutureTransaction() {
			var rT = new RecurringTransactionFactory().WithCreditDefaultValues().WithQuantity(1).WithCurrency(new Currency()).Build();
			var futureTransactions = rT.GetFutureTransaction();
			futureTransactions.Should().HaveCount(1);
		}



		[Theory]
		[InlineData(Periodicity.Weekly, 52)]
		[InlineData(Periodicity.Monthly, 12)]
		[InlineData(Periodicity.Bimonthly, 6)]
		[InlineData(Periodicity.Quarterly, 4)]
		[InlineData(Periodicity.Annually, 1)]
		public void RecurringTransactionWithoutQuantityShouldHaveFutureTransactionDependingOnPeriodicityForOneYear(Periodicity periodicity, int assertCount) {
			var rT = new RecurringTransactionFactory().WithCreditDefaultValues().WithPeriodicity(periodicity).WithStartDate(DateTime.Today.AddDays(-1)).WithCurrency(new Currency()).Build();
			var future = rT.GetFutureTransaction();
			future.Should().HaveCount(assertCount);
		}
	}

}