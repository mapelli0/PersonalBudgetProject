using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCleanArchitectureProject.Core.Entities;
using BaseCleanArchitectureProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salftech.SharedKernel.Interfaces;

namespace PersonalBudgetProject.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : BaseRepositoryController<Bank, Guid, BankDTO>
    {
		public BankController(IRepository<Bank, Guid> bankRepository): base(bankRepository) { }
	}
}