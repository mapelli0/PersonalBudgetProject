using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.Infrastructure.Data {

	public class Repository<T, TKey>: IRepository<T, TKey> where T: BaseEntityId<TKey>, IRoot {
		protected readonly BaseCleanArchitectureProjectDbContext _dbContext;
		protected readonly IMapper _autoMapper;
		private readonly IValidator<T> _validator;


		protected virtual async Task ValidateAsync (T entity, CancellationToken cancellationToken) {
			var validation = await this._validator.ValidateAsync(entity, cancellationToken);
			if (!validation.IsValid) {
				throw new ValidationException(validation.Errors);
			}
		}


		#region IRepository

		public Repository(BaseCleanArchitectureProjectDbContext dbContext, IMapper autoMapper, IValidator<T> validator) {
			_dbContext = dbContext;
			_autoMapper = autoMapper;
			_validator = validator;
		}

		#region Add


		public virtual async Task<T> AddAsync (T entity, CancellationToken cancellationToken = default) {
			await ValidateAsync(entity, cancellationToken);
			await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return entity;
		}

		public virtual async Task<IEnumerable<T>> BulkAddAsync (IEnumerable<T> entities, CancellationToken cancellationToken) {
			foreach (var entity in entities) {
				await this.ValidateAsync(entity, cancellationToken);
			}
			var bulkAddAsync = entities as T[] ?? entities.ToArray();
			await _dbContext.Set<T>().AddRangeAsync(bulkAddAsync, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return bulkAddAsync;
		}
		
		#endregion

		#region Delete

		public virtual async Task BulkDeleteAsync (IEnumerable<T> entities, CancellationToken cancellationToken) {
			_dbContext.Set<T>().RemoveRange(entities);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}



		public virtual async Task DeleteAsync (T entity, CancellationToken cancellationToken = default) {
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
		
		#endregion

		#region GetById

		public virtual Task<T> GetByIdAsync (TKey id, CancellationToken cancellationToken = default) {
			return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
		}

		public async Task<TDto> GetDTOByIdAsync<TDto> (TKey id, CancellationToken cancellationToken = default) where TDto : IDto<TKey> {
			return await _dbContext.Set<T>().ProjectTo<TDto>(_autoMapper.ConfigurationProvider).SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);
		}

		#endregion

		#region List

		public virtual async Task<QueryResult<T>> ListAsync (IQueryOptions<T> options = null, CancellationToken cancellationToken = default(CancellationToken)){
			var query = _dbContext.Set<T>().AsQueryable();
			var queryResult = await ApplyOptions<T>(query, options, cancellationToken);
			var list = await queryResult.Entities.ToListAsync(cancellationToken);
			return new QueryResult<T>(list, queryResult.Total);
			
		}

		public virtual async Task<QueryResult<TDto>> ListDTOAsync<TDto> (IQueryOptions<T> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			var query = _dbContext.Set<T>().AsQueryable();
			var queryResult = await ApplyOptions<T>(query, options, cancellationToken);
			var list = await queryResult.Entities.ProjectTo<TDto>(_autoMapper.ConfigurationProvider).ToListAsync(cancellationToken);
			return new QueryResult<TDto>(list, queryResult.Total);	
		}

		#endregion

		#region update

		public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default) {
			await this.ValidateAsync(entity,cancellationToken);
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public virtual async Task BulkUpdateAsync (IEnumerable<T> entities, CancellationToken cancellationToken) {
			foreach (var entity in entities) {
				await this.ValidateAsync(entity, cancellationToken);
			}
			_dbContext.Set<T>().UpdateRange(entities);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
		

		#endregion
		
		#endregion

		#region Apply options

		protected virtual async Task<QueryableResult<T>> ApplyOptions<T> (IQueryable<T> query, IQueryOptions<T> options, CancellationToken cancellationToken) where T: IBaseEntity {
			if (options?.Sorting != null) {
				query = ApplySorting(query, options.Sorting);
			}
			if (options?.Filter != null) {
				query = ApplyFilter(query, options.Filter);
			}
			var total = await query.CountAsync(cancellationToken);
			if (options?.Pagination != null) {
				query.Skip(options.Pagination.Skip).Take(options.Pagination.Take);
			}
			return new QueryableResult<T>(query, total);
		}

		protected virtual IQueryable<T> ApplyFilter<T> (IQueryable<T> query, IFilter<T> optionsFilter) where T: IBaseEntity {
			var result = query;
			return query.Where(optionsFilter.Filter);
		}

		protected virtual IQueryable<T> ApplySorting<T> (IQueryable<T> query, ISorting<T> optionsSorting) where T: IBaseEntity {
			var result = query;
			if (optionsSorting.Direction == ListSortDirection.Ascending) {
				result = query.OrderBy(optionsSorting.OrderBy);
			} else {
				result = query.OrderByDescending(optionsSorting.OrderBy);
			}
			return result;
		}

		#endregion
	}

}