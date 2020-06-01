using Salftech.SharedKernel.Interfaces;

namespace Salftech.SharedKernel {

	public abstract class BaseDTO<TKey>: IDto<TKey> {
		public TKey Id { get; private set; }
	}

}