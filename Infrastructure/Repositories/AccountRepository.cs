using Core.DTO;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(Testapp1Context dbContext)
            : base(dbContext)
        {

        }

        public async Task<FindAccountResponse> FindAccount(string number)
        {
            var response = await _context.Accounts.Include("Accounts").Where(w => w.Status == (byte)BasicStatuses.Ok).Select(s => new FindAccountResponse() { Owner = s.Owner, Balance = s.Balance, Overdraft = s.Overdraft,OperationsCount=s.Operations.Count,BasicStatus=BasicStatuses.Ok }).SingleAsync();
        
            return response??new FindAccountResponse() { BasicStatus=BasicStatuses.NotFound};

        }

        public dynamic GetReportData()
        {
            throw new NotImplementedException();
        }
    }
}
