using System;
using System.Linq;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using BaseCleanArchitectureProject.UnitTests.Factory;
using FluentAssertions;
using FluentDateTime;
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


		[Theory]
		[InlineData(Periodicity.Weekly, 51)]
		[InlineData(Periodicity.Monthly, 11)]
		[InlineData(Periodicity.Bimonthly, 5)]
		[InlineData(Periodicity.Quarterly, 3)]
		[InlineData(Periodicity.Annually, 0)]
		public void RecurringTransactionWithTransactionRegisteredOnCurrentPeriodShouldReturnOnlyFuture (Periodicity periodicity, int assertCount) {
			var passedTransactionDate = DateTime.Today;
			var recurStartDate = DateTime.Today.AddDays(-1);
			var rT = new RecurringTransactionFactory().WithCreditDefaultValues().WithPeriodicity(periodicity).WithStartDate(recurStartDate).WithCurrency(new Currency()).WithPassedTransactionOn(passedTransactionDate).Build();
			var future = rT.GetFutureTransaction();
			var dates = GetLimitDates(periodicity, passedTransactionDate);
			future.Should().HaveCount(assertCount);
			future.Where(f => dates.Item1 < f.Date && f.Date < dates.Item2).Should().BeEmpty();
		}

		private Tuple<DateTime, DateTime> GetLimitDates (Periodicity periodicity, in DateTime passedTransactionDate) {
			DateTime initialDate;
			DateTime finalDate;
			switch (periodicity) {
				case Periodicity.Weekly:
					initialDate = passedTransactionDate.BeginningOfWeek();
					finalDate = passedTransactionDate.EndOfWeek();
					break;
				case Periodicity.Monthly:
					initialDate = passedTransactionDate.BeginningOfMonth();
					finalDate = passedTransactionDate.EndOfMonth();
					break;
				case Periodicity.Bimonthly:
					initialDate = passedTransactionDate.BeginningOfMonth();
					finalDate = initialDate.AddMonths(1).EndOfMonth();
					break;
				case Periodicity.Quarterly:
					initialDate = passedTransactionDate.BeginningOfMonth();
					finalDate = initialDate.AddMonths(2).EndOfMonth();
					break;
				case Periodicity.Annually:
					initialDate = passedTransactionDate.BeginningOfYear();
					finalDate = passedTransactionDate.EndOfYear();
					break;
				default:
					initialDate = passedTransactionDate;
					finalDate = passedTransactionDate.AddDays(1);
					break;
			}
			return new Tuple<DateTime, DateTime>(initialDate, finalDate);
		}
	}

}