using System;
using System.Runtime.CompilerServices;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class Transaction : BaseGuidEntity {
		
		public TransactionType TransactionType { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public Guid AccountId { get; set; }

		public Account Account { get; set; }

		public DateTime Date { get; set; }

		public double Value { get; set; }
	}

}