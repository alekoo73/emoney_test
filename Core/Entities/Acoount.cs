﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Acoount
    {
        public Acoount()
        {
            OpearationsLogs = new HashSet<OpearationsLog>();
            Operations = new HashSet<Operation>();
        }

        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Owner { get; set; }
        public decimal Overdraft { get; set; }

        public virtual ICollection<OpearationsLog> OpearationsLogs { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}