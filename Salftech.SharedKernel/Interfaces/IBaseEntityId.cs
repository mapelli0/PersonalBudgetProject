namespace Salftech.SharedKernel {

	public interface IBaseEntityId<TKey>: IBaseEntity {
		TKey Id { get; set; }
	}

}