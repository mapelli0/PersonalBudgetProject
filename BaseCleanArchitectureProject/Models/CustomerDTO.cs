using System;
using Microsoft.AspNetCore.Authentication;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.Models {

	public class CustomerDTO : BaseGuidDTO {
		public string Name { get; set; }

		public string AdministratorName { get; set; }
	}

}