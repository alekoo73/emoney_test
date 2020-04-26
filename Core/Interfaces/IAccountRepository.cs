using Core.DTO;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{   
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<FindAccountResponse> FindAccount(string number);

        public Task<TransferResponse> Transfer(TransferRequest request,IAppLogger logger);
        public Task<dynamic> GetReportData();

    }
}
