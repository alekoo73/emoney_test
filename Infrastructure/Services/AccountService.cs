using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Core.DTO;
using Core.Entities;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        public readonly IAppLogger<AccountService> _appLogger;

        public AccountService(IUnitOfWork uow,IAppLogger<AccountService> appLogger)
        {
            this._uow = uow;
            this._appLogger = appLogger;
        }

        public async Task<FindAccountResponse> FindAccount(string idnumber)
        {
           return await ((IAccountRepository)_uow.Repository<IAccountRepository, Account>()).FindAccount(idnumber);
        }

        public dynamic GetReportData()
        {
            throw new NotImplementedException();
        }
    }
}
