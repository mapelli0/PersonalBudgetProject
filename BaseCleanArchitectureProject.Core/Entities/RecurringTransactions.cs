using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using FluentDateTime;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class RecurringTransaction : BaseGuidEntity, IRoot {
		private readonly ICollection<Transaction> _recordedTransactions;

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

		public Guid? CategoryId { get; set; }

		public Category Category { get; set; }

		public IEnumerable<Transaction> GetFutureTransaction(DateTime? until = null) {
			var result = new List<Transaction>();
			if (!until.HasValue) {
				until = DateTime.Today.AddDays(-1).NextYear();
			}
			var start = DateTime.Today;

			while ((start < until.Value)) {
				if (AddCurrent(start)) {
					result.Add(new Transaction() {
														Name = $"{this.Name} {start:dd/MM/yyyy}",
														Date = start,
														Description = $"Future transaction {this.Name}",
														Value = this.Value
												});
				}
				if (Quantity != 0 && result.Count() - Quantity >= 0) {
					break;
				}
				start = NextOccurence(start);
			}
			return result;

		}

		private bool AddCurrent (in DateTime start) {
			var limitDates = GetLimitDates(start);
			return !this.RecordedTransactions.Any(rt => limitDates.Item1 <= rt.Date && rt.Date <= limitDates.Item2);
		}

		private Tuple<DateTime, DateTime> GetLimitDates (in DateTime passedTransactionDate) {
			DateTime initialDate;
			DateTime finalDate;
			switch (Periodicity) {
				case Periodicity.Weekly:
					initialDate = passedTransactionDate.BeginningOfWeek();
					finalDate = passedTransactionDate.EndOfWeek();
					break;
				case Periodicity.Monthly:
					initialDate = passedTransactionDate.BeginningOfMonth();
					finalDate = passedTransactionDate.EndOfMonth();
					break;
				case Periodicity.Bimonthly:
					initialDate = passedTransactionDate.BeginningOfMonth();
					finalDate = initialDate.AddMonths(1).EndOfMonth();
					break;
				case Periodicity.Quarterly:
					initialDate = passedTransactionDate.BeginningOfMonth();
					finalDate = initialDate.AddMonths(2).EndOfMonth();
					break;
				case Periodicity.Annually:
					initialDate = passedTransactionDate.BeginningOfYear();
					finalDate = passedTransactionDate.EndOfYear();
					break;
				default:
					initialDate = passedTransactionDate;
					finalDate = passedTransactionDate.AddDays(1);
					break;
			}
			return new Tuple<DateTime, DateTime>(initialDate,finalDate);
		}


		public void AddTransaction (Transaction transaction) {
			this._recordedTransactions.Add(transaction);
		}

		private DateTime NextOccurence (in DateTime start) {
			DateTime result;
			switch (Periodicity) {
				case Periodicity.Weekly:
					result = start.WeekAfter();
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

}