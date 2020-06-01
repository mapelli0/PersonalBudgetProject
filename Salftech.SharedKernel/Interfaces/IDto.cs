namespace Salftech.SharedKernel.Interfaces {

	public interface IDto<out TKey> {
		TKey Id { get; }
	}

}