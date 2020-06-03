using System;
using System.Collections.Generic;
using MediatR;

namespace Salftech.SharedKernel {

	public interface IBaseEntity {
		DateTime Created { get; set; }

		DateTime Updated { get; set; }

		List<IRequest> Events { get; }

		public void Update();
	}

}