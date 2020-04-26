using Core.DTO;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

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
            var response = await _context.Accounts.AsNoTracking().Include(m => m.Operations).Where(w => w.Status == (byte)BasicStatuses.Ok && w.AccountId == number).Select(s => new FindAccountResponse() { Owner = s.Owner, Balance = s.Balance, Overdraft = s.Overdraft, OperationsCount = s.Operations.Count, Status = BasicStatuses.Ok }).SingleOrDefaultAsync();

            return response ?? new FindAccountResponse() { Status = BasicStatuses.NotFound };

        }

        public async Task<dynamic> GetReportData()
        {
            return await _context.Accounts.AsNoTracking().Include(m => m.Operations).Where(w => w.Status == (byte)BasicStatuses.Ok).Select(s => new { Owner = s.Owner, Balance = s.Balance, Overdraft = s.Overdraft, OperationsCount = s.Operations.Count, Debit=s.Operations.Where(o=>o.OperationAmount>0).Sum(s=>s.OperationAmount), Credit = s.Operations.Where(o => o.OperationAmount < 0).Sum(s => s.OperationAmount) }).ToListAsync();       
        }

        public async Task<TransferResponse> Transfer(TransferRequest request, IAppLogger logger)
        {
            TransferResponse response = new TransferResponse() {Status=BasicStatuses.Ok };

            using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    var accountrec = await _context.Accounts.AsNoTracking().SingleOrDefaultAsync(w => w.Status == (byte)BasicStatuses.Ok && w.AccountId == request.Account);

                    if (accountrec == null)
                    {
                        response.Status = BasicStatuses.NotFound;
                        return response;
                    }

                    if (request.ActualAmount < 0 && accountrec.Balance + accountrec.Overdraft + request.ActualAmount < 0)
                    {
                        response.Balance = accountrec.Balance;
                        response.Status = BasicStatuses.NotEnough;
                        return response;
                    }

                    if (await _context.Operations.FindAsync(request.TransactionId)!=null)
                    {
                        response.Balance = accountrec.Balance;
                        response.Status = BasicStatuses.Duplicate;
                        return response;
                    }

                    await _context.Operations.AddAsync(new Operation() { AccountId = accountrec.AccountId, TransactionId = request.TransactionId, OperationAmount = request.ActualAmount });

                    accountrec.Balance = accountrec.Balance + request.ActualAmount;
                    response.Balance = accountrec.Balance;
                    _context.Update(accountrec);
                    await _context.OpearationsLogs.AddAsync(new OpearationsLog() { AccountId = accountrec.AccountId,OperationDateTime=DateTime.Now,IpAddress=request.IpAddress, Request = JsonSerializer.Serialize(request),Response= JsonSerializer.Serialize(response), Status=(byte)BasicStatuses.Ok});


                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception.ParseException());
                    response.Status = BasicStatuses.Failed;
                }
            }
            return response;
        }
    }
}
