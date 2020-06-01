using BaseCleanArchitectureProject.Models;

namespace BaseCleanArchitectureProject.Core.Entities.Mapper {

	public class CustomerProfile : AutoMapper.Profile{
		public CustomerProfile() {
			CreateMap<Customer.Customer, CustomerDTO>().ForMember(d => d.AdministratorName, opt => opt.MapFrom(s => s.Administrator.UserName));
		}
	}

}