using Core.Enums;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class TransferRequest: IDTO
    {
        [RegularExpression("^(36)([0-9]{6})$", ErrorMessage = "Bad Account")]
        public string Account { get; set; }

        [RegularExpression("^(UID)-([0-9]{4})$", ErrorMessage = "Bad Transaction Id")]
        public string TransactionId { get; set; }

        [Range(0, 1, ErrorMessage = "Bad OperationType")]
        public byte OperationType { get; set; }

        [Range(100, 100000000, ErrorMessage = "Bad Amount, sent in tetris")]
        public int OperationAmount { get; set; }

        
        public string IpAddress { get; set;}

        public decimal ActualAmount => ((decimal)OperationAmount / 100) * ((OpType)OperationType == OpType.Income ? 1 : -1);
    }
}