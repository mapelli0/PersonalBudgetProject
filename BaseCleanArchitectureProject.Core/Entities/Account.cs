using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BaseCleanArchitectureProject.Core.Entities.Enums;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class Account: BaseGuidEntity, IRoot {
		private readonly ICollection<Transaction> _transactions;

		public Account() {
			_transactions = new HashSet<Transaction>();
		}

		public IEnumerable<Transaction> Transactions => _transactions.ToList().AsReadOnly();

		[Required]
		public string Name { get; set; }

		[Required]
		public string Number { get; set; }

		public AccountType AccountType { get; set; }

		[DefaultValue(0)]
		public double InitialBalance { get; set; }

		[Required]
		public Guid BankId { get; set; }

		public Bank Bank { get; set; }

		public void AddCredit (Transaction transaction) {
			transaction.TransactionType = TransactionType.Credit;
			AddTransaction(transaction);
		}
		
		public void AddDebit (Transaction transaction) {
			transaction.TransactionType = TransactionType.Debit;
			AddTransaction(transaction);
		}

		private void AddTransaction (Transaction transaction) {
			transaction.Account = this;
			this._transactions.Add(transaction);
		}

		public Guid CurrencyId { get; set; }

		[Required]
		public Currency Currency { get; set; }


		public double GetCurrentBalance() {
			var totalCredit = this.Transactions.Where(t => t.TransactionType == TransactionType.Credit).Sum(s => s.Value);
			var totalDebit = this.Transactions.Where(t => t.TransactionType == TransactionType.Debit).Sum(s => s.Value);
			return this.InitialBalance + (totalCredit - totalDebit);
		} 


	}

}