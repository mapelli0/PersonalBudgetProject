using System;
using Salftech.SharedKernel.Interfaces;

namespace Salftech.SharedKernel {

	public abstract class BaseGuidDTO : IDto<Guid> {
		public Guid Id { get; set; }
	}

}