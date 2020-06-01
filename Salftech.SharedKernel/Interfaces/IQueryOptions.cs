using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Salftech.SharedKernel.Interfaces {

	public interface IQueryOptions<T> where T: IBaseEntity {
		IPagination Pagination { get; set; }

		ISorting<T> Sorting { get; set; }

		IFilter<T> Filter { get; set; }
	}

	public interface IFilter<T> where T: IBaseEntity {
		Expression<Func<T, bool>> Filter { get; set; }
	}

	public interface ISorting<T> where T: IBaseEntity {
		public Expression<Func<T, object>> OrderBy { get; }

		public ListSortDirection Direction { get; }
	}

	public interface IPagination {
		int Skip { get; set; }

		int Take { get; set; }
	}

	public interface IResult<out TResultType> {
		TResultType Entities { get; }

		int Total { get; }
	}

	public class QueryableResult<T>: IResult<IQueryable<T>> {
		public QueryableResult (IQueryable<T> entities, int total) {
			Entities = entities;
			Total = total;
		}

		public IQueryable<T> Entities { get; }

		public int Total { get; }
	}

	public class QueryResult<T>: IResult<IEnumerable<T>> {
		public QueryResult (IEnumerable<T> entities, int total) {
			Entities = entities;
			Total = total;
		}

		public IEnumerable<T> Entities { get; }

		public int Total { get; }
	}

}