using System.ComponentModel.DataAnnotations;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class Currency : BaseGuidEntity {

		[Required]
		public string Name { get; set; }

		[Required]
		public string Country { get; set; }

		[Required]
		public string Symbol { get; set; }

		[Required]
		public string ISO { get; set; }
		
	}

}