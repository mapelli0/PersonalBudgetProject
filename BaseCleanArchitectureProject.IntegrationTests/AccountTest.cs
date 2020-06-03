using System;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Institution;
using BaseCleanArchitectureProject.Infrastructure.Data;
using BaseCleanArchitectureProject.UnitTests.Factory;
using FluentAssertions;
using Xunit;

namespace BaseCleanArchitectureProject.IntegrationTests {

	public class AccountTest : BaseEfRepositoryTestFixture {
		[Fact]
		public async void AddAccountWithoutValuesShouldTrow() {
			var repo = await GetRepository<Account>();
			var acc = new AccountFactory().WithName().WithNumber().Build();
			Func<Task> add = async () => {
								await repo.AddAsync(acc,default);
							};
			add.Should().Throw<FluentValidation.ValidationException>();
		}

		[Fact]
		public async void AddAccountUsingBankShouldNotTrow() {
			var bank = new BankFactory().WithName("Test Name Bank").Build();
			var repoBank = await GetRepository<Bank>();
			bank = await repoBank.AddAsync(bank, default);
			var acc = new AccountFactory().WithName("Check account").WithNumber("910-5").Build();
			bank.AddAccount(acc);
			Func<Task> update = async () => {
									await repoBank.UpdateAsync(bank, default);
								};
			update.Should().NotThrow();

			acc.Bank.Should().Be(bank);
			acc.BankId.Should().Be(bank.Id);
			acc.Bank.Name.Should().Be(bank.Name);
		}

		[Fact]
		public async void AddAccountShouldHaveBank() {
			var repo = await GetRepository<Account>();
			var acc = new AccountFactory().WithName("Check account").WithNumber("910-5").Build();
			Func<Task> add = async () => {
								await repo.AddAsync(acc,default);
							};
			add.Should().Throw<FluentValidation.ValidationException>();
		}

		[Fact]
		public async void AddAccountWithValuesShouldNotThrow() {
			var acc = new AccountFactory().WithDefaultValues().Build();
			var repo = await GetRepository<Account>();
			Func<Task> add = async () => {
								await repo.AddAsync(acc,default);
							};
			add.Should().NotThrow();
		}

		[Fact]
		public async void GetAccountShouldIncludeTransactionByDefault() {
			var acc = new AccountFactory().WithDefaultValues().Build();
			var repo = await GetRepository<Account>();
			var newAcc = await repo.AddAsync(acc, default);
			var trans1 = new TransactionFactory().WithDefaultValues().Build();
			newAcc.AddCredit(trans1);
			var trans2 = new TransactionFactory().WithDefaultValues().Build();
			newAcc.AddDebit(trans2);
			await repo.UpdateAsync(newAcc, default);

			var rAcc = await repo.GetByIdAsync(newAcc.Id, default);

			rAcc.Transactions.Should().Contain(trans2);
			rAcc.Transactions.Should().Contain(trans1);
		}
		[Fact]
		public async void AccountBalanceShouldBeTheSumOfInitialBalanceCreditsAndDebits() {
			var acc = new AccountFactory().WithDefaultValues().WithInitialBalance(1000).Build();
			var repo = await GetRepository<Account>();
			var newAcc = await repo.AddAsync(acc, default);
			var trans1 = new TransactionFactory().WithDefaultValues().Build();
			newAcc.AddCredit(trans1);
			var trans2 = new TransactionFactory().WithDefaultValues().Build();
			newAcc.AddDebit(trans2);
			await repo.UpdateAsync(newAcc, default);

			var total = acc.InitialBalance + trans1.Value - trans2.Value;

			newAcc.Transactions.Should().NotBeEmpty();
			newAcc.GetCurrentBalance().Should().Be(total);
		}



	}

}