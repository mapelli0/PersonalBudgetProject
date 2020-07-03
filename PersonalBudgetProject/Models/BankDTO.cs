using System;
using System.Collections.Generic;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Models {

	public class BankDTO : BaseDTO<Guid> {
		public string Name { get; set; }
	}

}