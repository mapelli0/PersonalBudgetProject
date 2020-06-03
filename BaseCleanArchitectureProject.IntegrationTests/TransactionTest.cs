

using System;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.UnitTests.Factory;
using FluentAssertions;
using Xunit;

namespace BaseCleanArchitectureProject.IntegrationTests {

	public class TransactionTest : BaseEfRepositoryTestFixture {


		[Fact]
		public async Task TransactionShouldBeAddedUsingAccount() {
			var acc = new AccountFactory().WithDefaultValues().Build();
			var transaction = new TransactionFactory().WithDefaultValues().Build();
			acc.AddCredit(transaction);

			var repo = await GetRepository<Account>();
			Func<Task> add = async () => {
								await repo.AddAsync(acc, default);
							};
			add.Should().NotThrow();
			acc.Transactions.Should().HaveCount(1);
			acc.GetCurrentBalance().Should().Be(transaction.Value);
		}

	}

}