using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace PersonalBudgetProject.Endpoints {

	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseRepositoryController<T, TKey, TDto>: ControllerBase where T: IBaseEntityId<TKey>, IRoot where TDto: BaseDTO<TKey> {
		private readonly IRepository<T, TKey> _repository;

		protected BaseRepositoryController (IRepository<T, TKey> repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<List<TDto>>> Get (CancellationToken cancellationToken) {
			var list = await _repository.ListDTOAsync<TDto>(null, cancellationToken);
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TDto>> Get (TKey id, CancellationToken cancellationToken) {
			var bank = await _repository.GetDTOByIdAsync<TDto>(id, cancellationToken);
			return Ok(bank);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete (TKey id, CancellationToken cancellationToken) {
			await _repository.DeleteAsync(id, cancellationToken);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<TDto>> Post (TDto bank, CancellationToken cancellationToken) {
			var dto = await _repository.AddDTOAsync(bank, cancellationToken);
			return Ok(dto);
		}

		[HttpPut]
		public async Task<ActionResult<bool>> Put (TDto bank, CancellationToken cancellationToken) {
			await _repository.UpdateDTOAsync(bank, cancellationToken);
			return Ok();
		}
	}

}