using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace PersonalBudgetProject.Endpoints {

	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseServiceController<TService, T, TKey, TDto>: ControllerBase where TService: IService<T, TKey>, new() where TDto: BaseDTO<TKey> where T: IBaseEntityId<TKey>, IRoot {
		private readonly TService _service;

		protected BaseServiceController (TService service) {
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<TDto>>> Get (CancellationToken cancellationToken) {
			var list = await _service.ListDTOAsync<TDto>(null, cancellationToken);
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TDto>> Get (TKey id, CancellationToken cancellationToken) {
			var bank = await _service.GetDTOByIdAsync<TDto>(id, cancellationToken);
			return Ok(bank);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete (TKey id, CancellationToken cancellationToken) {
			await _service.DeleteAsync(id, cancellationToken);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<TDto>> Post (TDto bank, CancellationToken cancellationToken) {
			var dto = await _service.AddDTOAsync(bank, cancellationToken);
			return Ok(dto);
		}

		[HttpPut]
		public async Task<ActionResult<bool>> Put (TDto bank, CancellationToken cancellationToken) {
			await _service.UpdateDTOAsync(bank, cancellationToken);
			return Ok();
		}
	}

}