using System;
using System.Threading;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Customer;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using BaseCleanArchitectureProject.Core.Events;
using BaseCleanArchitectureProject.Core.Interfaces;
using BaseCleanArchitectureProject.Infrastructure.Data;
using BaseCleanArchitectureProject.Models;
using BaseCleanArchitectureProject.UnitTests.Builders;
using BaseCleanArchitectureProject.UnitTests.Factory;
using FluentAssertions;
using Moq;
using Salftech.SharedKernel;
using Xunit;

namespace BaseCleanArchitectureProject.IntegrationTests
{
    public class CustomerTest : BaseEfRepositoryTestFixture
    {
        [Fact]
        public async Task AddACustomerShouldAddUserAndDispatchAddCustomerEvent() {
			var repo = await GetRepositoryWithMocks<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().Build();
			var addedCustomer = await repo.AddAsync(customer, default);
			addedCustomer.Id.Should().NotBeEmpty();
			addedCustomer.CompanyName.Should().Be(customer.CompanyName);
			addedCustomer.Administrator.Should().NotBeNull();
			this._dispatcherMock.Verify(d => d.Send(It.IsAny<AddCustomerEvent>(), default), Times.Once);
		}

		[Fact]
		public async Task AddCustomerDispatcherShouldNotThrowNullError() {
			var repo = await GetRepository<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().Build();
			Func<Task> add = async () => {
								await repo.AddAsync(customer, default);
							};
			add.Should().NotThrow();
		}

		[Fact]
		public async Task GetCustomerDTOShouldMapAdministratorName() {
			var repo = await GetRepository<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().Build();
			var addedCustomer = await repo.AddAsync(customer, default);
			var getCustomerDto = await repo.GetDTOByIdAsync<CustomerDTO>(addedCustomer.Id);
			getCustomerDto.AdministratorName.Should().NotBeEmpty();

		}

		[Fact]
		public async Task AddUserToCustomerShouldSendEmail() {
			var repo = await GetRepository<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().Build();
			var addedCustomer = await repo.AddAsync(customer, default);
			addedCustomer.Id.Should().NotBeEmpty();
			addedCustomer.CompanyName.Should().Be(customer.CompanyName);
			addedCustomer.Administrator.Should().NotBeNull();
			
			Func<Task> add = async () => {
								await repo.AddUserAsync(addedCustomer,
												new ApplicationUser("alfredo.user") {
																							Email = "alfredo.mapelli+user@gmail.com",
																							InitialPassword = "Aq112233"
																					},
												default);
							};
			add.Should().NotThrow();
			addedCustomer.Users.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task AddUserWithWrongEmailShouldThrowError() {
			var repo = await GetRepository<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().WithEmail("wrong email").Build();
			Func<Task> add = async () => {
								await repo.AddAsync(customer, default);
							};
			add.Should().Throw<FluentValidation.ValidationException>();
		}

		[Fact]
		public async Task UpdateUserWithWrongEmailShouldThrowError() {
			var repo = await GetRepository<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().Build();
			await repo.AddAsync(customer, default);

			customer.Email = "now it is wrong";
			Func<Task> update = async () => {
								await repo.UpdateAsync(customer, default);
							};
			update.Should().Throw<FluentValidation.ValidationException>();
		}


		[Fact]
		public async Task AddCustomerWithMissingAddressInformationShouldThrowError() {
			var repo = await GetRepository<CustomerRepository, Customer>();
			var customer = new CustomerFactory().WithDefaultValues().WithMissingAddressInformation().Build();
			Func<Task> add = async () => {
								await repo.AddAsync(customer, default);
							};
			add.Should().Throw<FluentValidation.ValidationException>();
		}




    }
}
