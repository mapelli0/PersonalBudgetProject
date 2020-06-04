using System;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.UnitTests.Factory;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace BaseCleanArchitectureProject.IntegrationTests {

	public class BankTest: BaseEfRepositoryTestFixture {
		[Fact]
		public async Task AddBankWithoutNameShouldGiveError() {
			var repo = await GetRepository<Bank>();
			var bank = new BankFactory().WithName().Build();
			Func<Task> add = async () => {
								await repo.AddAsync(bank, default);
							};
			add.Should().Throw<ValidationException>();
		}

		[Fact]
		public async Task GetBankShouldIncludeAccountsByDefault() {
			var repo = await GetRepository<Bank>();
			var bank = new BankFactory().WithName("Test Bank").Build();
			var addedBank = await repo.AddAsync(bank, default);
			var acc = new AccountFactory().WithDefaultValues().Build();
			addedBank.AddAccount(acc);
			await repo.UpdateAsync(bank, default);
			var rBank = await repo.GetByIdAsync(addedBank.Id, default);
			rBank.Accounts.Should().NotBeEmpty();
		}
	}

}