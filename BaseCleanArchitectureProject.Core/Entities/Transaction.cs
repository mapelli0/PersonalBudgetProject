using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class Transaction : BaseGuidEntity {
		
		public TransactionType TransactionType { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public Guid AccountId { get; set; }

		[Required]
		public Account Account { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public double Value { get; set; }
	}

}