using System;
using System.Threading;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Core.Entities.Customer;
using BaseCleanArchitectureProject.Models;
using Microsoft.AspNetCore.Mvc;
using Salftech.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace BaseCleanArchitectureProject.Endpoints {

	[ApiController]
	[Route("endpoint/[controller]")]
	[Produces("application/json")]
	public class CustomerController : ControllerBase{
		private readonly IRepository<Customer, Guid> _repository;


		public CustomerController (IRepository<Customer, Guid> repository) {
			_repository = repository;
		}


		[SwaggerOperation(Summary = "List add Customers",
						Description = "List add Customers",
						OperationId = "Customer.List",
						Tags = new[] {
											"CustomerEndpoint"
									})]
		public async Task<IActionResult> Get (int id, CancellationToken cancellationToken) {
			try {
				var list = await _repository.ListDTOAsync<CustomerDTO>(null, cancellationToken);
				return Ok(list);
			} catch(Exception e) {
				return BadRequest(e.Message);
			}
			
		}

	}

}