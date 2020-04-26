using Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        public Task<FindAccountResponse> FindAccount(string idnumber);
        public Task<TransferResponse> Transfer(TransferRequest request);
        public Task<dynamic> GetReportData();
    }
}
