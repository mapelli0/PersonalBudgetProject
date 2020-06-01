using System.ComponentModel.DataAnnotations;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities.Address {

	public class Address : BaseGuidEntity {
		[Required]
		public string Postcode { get; set; }

		[Required]
		public string Address1 { get; set; }

		public string Address2 { get; set; }

		public string Address3 { get; set; }

		[Required]
		public string City { get; set; }

	}

}