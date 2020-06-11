using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities {

	public class Category : BaseGuidEntity, IRoot {
		private readonly ICollection<RecurringTransaction> _recurringTransactions;
		private readonly ICollection<Transaction> _transactions;
		private readonly ICollection<Category> _subCategories;

		public Category() {
			this._recurringTransactions = new HashSet<RecurringTransaction>();
			this._transactions = new HashSet<Transaction>();
			this._subCategories = new HashSet<Category>();
		}

		public string Name { get; set; }

		public IEnumerable<RecurringTransaction> RecurringTransactions => this._recurringTransactions.ToList().AsReadOnly();
		public IEnumerable<Transaction> Transactions => this._transactions.ToList().AsReadOnly();
		public IEnumerable<Category> SubCategories => this._subCategories.ToList().AsReadOnly();

		public Guid ParentCategoryId { get; set; }

		public Category ParentCategory { get; set; }

	}

}