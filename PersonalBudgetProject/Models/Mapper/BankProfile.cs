using AutoMapper;
using BaseCleanArchitectureProject.Core.Entities;

namespace BaseCleanArchitectureProject.Models.Mapper {

	public class BankProfile : Profile{
		public BankProfile() {
			CreateMap<Bank, BankDTO>().ReverseMap();
		} 
	}

}