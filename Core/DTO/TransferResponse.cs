using Core.Enums;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Core.DTO
{
    public class TransferResponse
    {
        private BasicStatuses _basicStatus;
        public BasicStatuses BasicStatus
        {
            get
            {
                return _basicStatus;
            }
            set
            {
                Contract.Ensures(Contract.Result<BasicStatuses>().In(BasicStatuses.Ok,BasicStatuses.Duplicate, BasicStatuses.NotFound,BasicStatuses.NotEnough));
                _basicStatus = value;
            }
        }
        public decimal Balance { get; set; }
    }
}
