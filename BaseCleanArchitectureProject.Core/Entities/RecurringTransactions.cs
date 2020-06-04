using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class RecurringTransaction : BaseGuidEntity, IRoot {
		private ICollection<Transaction> _recordedTransactions;

		public RecurringTransaction() {
			_recordedTransactions = new HashSet<Transaction>();
		}

		public TransactionType RecurringTransactionType { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		public int Quantity { get; set; }

		public Periodicity Periodicity { get; set; }

		public bool IsArchived { get; set; }

		public IEnumerable<Transaction> RecordedTransactions => this._recordedTransactions.ToList().AsReadOnly();

		public Guid CurrencyId { get; set; }

		[Required]
		public Currency Currency { get; set; }

		[Required]
		public double Value { get; set; }

		public IEnumerable<Transaction> GetFutureTransaction(DateTime? until = null) {
			var result = new List<Transaction>();
			if (!until.HasValue) {
				until = DateTime.Today.AddYears(1);
			}
			var start = NextOccurence(DateTime.Today);

			while (AddFuture(start, until.Value, result)) {
				start = NextOccurence(start);
				result.Add(new Transaction() {
													Name = $"{this.Name} {start:dd/MM/yyyy}",
													Date = start,
													Description = $"Future transaction {this.Name}",
													Value = this.Value
											});
			}
			return result;

		}

		private bool AddFuture (in DateTime start, DateTime until, List<Transaction> futureTransactions) {
			if (start <= until) {
				if (Quantity == 0) {
					return true;
				} else {
					return Quantity - (RecordedTransactions.Count() + futureTransactions.Count()) > 0;
				}

			} else {
				return false;
			}
		}

		private DateTime NextOccurence (in DateTime start) {
			DateTime result;
			switch (Periodicity) {
				case Periodicity.Weekly:
					result = start.AddDays(7);
					break;
				case Periodicity.Monthly:
					result = start.AddMonths(1);
					break;
				case Periodicity.Bimonthly:
					result = start.AddMonths(2);
					break;
				case Periodicity.Quarterly:
					result = start.AddMonths(3);
					break;
				case Periodicity.Annually:
					result = start.AddYears(1);
					break;
				default:
					result = start.AddDays(1);
					break;
			}
			return result;
		}
	}

	public enum Periodicity {
		Weekly,
		Monthly,
		Bimonthly,
		Quarterly,
		Annually
	}

}