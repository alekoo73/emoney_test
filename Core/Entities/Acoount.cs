﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public partial class Account
    {
        public Account()
        {
            OpearationsLogs = new HashSet<OpearationsLog>();
            Operations = new HashSet<Operation>();
        }

        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Owner { get; set; }
        public decimal Overdraft { get; set; }        
        public byte Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<OpearationsLog> OpearationsLogs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Operation> Operations { get; set; }
    }
}