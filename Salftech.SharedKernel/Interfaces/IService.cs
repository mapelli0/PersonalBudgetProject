using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Salftech.SharedKernel.Interfaces {

	public interface IService<T, TKey> where T: IBaseEntity {
		
		Task<TDto> GetDTOByIdAsync<TDto> (TKey id, CancellationToken cancellationToken = default) where TDto: IDto<TKey>;

		Task<QueryResult<TDto>> ListDTOAsync<TDto> (IQueryOptions<T> options = null, CancellationToken cancellationToken = default(CancellationToken));

		Task<TDto> AddDTOAsync<TDto> (TDto dto, CancellationToken cancellationToken = default(CancellationToken));

		Task UpdateDTOAsync<TDto> (TDto dto, CancellationToken cancellationToken = default(CancellationToken));

		Task DeleteAsync (TKey id, CancellationToken cancellationToken);

	}

}