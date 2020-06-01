using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Salftech.SharedKernel {

	public abstract class BaseEntityId<TKey> : BaseEntity, IBaseEntityId<TKey> {
		public TKey Id { get; set; }
	}

	public abstract class BaseEntity: IBaseEntity {
		protected BaseEntity() {
			Created = DateTime.Now;
			Events = new List<IRequest>();
		}

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }

		[NotMapped]
		public List<IRequest> Events { get; set; }

		#region IBaseEntity

		public void Update() {
			Updated = DateTime.Now;
		}

		#endregion
	}

}