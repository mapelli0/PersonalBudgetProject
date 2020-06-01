using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Salftech.SharedKernel.Interfaces {

	public interface IRepository<T, TKey> where T: BaseEntityId<TKey> {

		Task<T> GetByIdAsync (TKey id, CancellationToken cancellationToken);

		Task<TDto> GetDTOByIdAsync<TDto> (TKey id, CancellationToken cancellationToken = default) where TDto: IDto<TKey>;

		Task<QueryResult<T>> ListAsync (IQueryOptions<T> options = null, CancellationToken cancellationToken = default(CancellationToken));

		Task<QueryResult<TDto>> ListDTOAsync<TDto> (IQueryOptions<T> options = null, CancellationToken cancellationToken = default(CancellationToken));

		Task<T> AddAsync (T entity, CancellationToken cancellationToken);

		Task<IEnumerable<T>> BulkAddAsync (IEnumerable<T> entities, CancellationToken cancellationToken);

		Task UpdateAsync (T entity, CancellationToken cancellationToken);

		Task BulkUpdateAsync (IEnumerable<T> entities, CancellationToken cancellationToken);

		Task DeleteAsync (T entity, CancellationToken cancellationToken);

		Task BulkDeleteAsync (IEnumerable<T> entities, CancellationToken cancellationToken);
	}

}