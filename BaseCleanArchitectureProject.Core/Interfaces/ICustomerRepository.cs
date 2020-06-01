using System;
using System.Threading;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Customer;
using BaseCleanArchitectureProject.Core.Entities.Identity;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.Core.Interfaces {

	public interface ICustomerRepository : IRepository<Customer, Guid> {
		Task AddUserAsync (Customer customer, ApplicationUser applicationUser, CancellationToken cancellationToken);
	}

}