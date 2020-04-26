using Core.Enums;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Core.DTO
{
    public class FindAccountResponse
    {
        private BasicStatuses _basicStatus;
        public BasicStatuses BasicStatus
        {
            get
            {
                return _basicStatus;
            }
            set {
                Contract.Ensures(Contract.Result<BasicStatuses>().In(BasicStatuses.Ok,BasicStatuses.NotFound));
                _basicStatus = value;
            }
        }
        public string Owner { get; set; }
        public decimal? Overdraft { get; set; }
        public decimal? Balance { get; set; }
        public int? OperationsCount { get; set; }
    }
}
