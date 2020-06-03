using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Salftech.SharedKernel;

namespace BaseCleanArchitectureProject.Core.Entities.Institution {

	public class Bank : BaseGuidEntity, IRoot {
		private readonly ICollection<Account> _accounts;

		public Bank() {
			_accounts = new HashSet<Account>();
		}

		[Required]
		public string Name { get; set; }

		public IEnumerable<Account> Accounts => _accounts.ToList().AsReadOnly();

		public void AddAccount (Account account) {
			account.Bank = this;
			this._accounts.Add(account);
		}

	}

}