﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public partial class Testapp1Context : DbContext
    {
        public Testapp1Context()
        {
        }

        public Testapp1Context(DbContextOptions<Testapp1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<OpearationsLog> OpearationsLogs { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ser2008;Initial Catalog=Testapp1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.AccountId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Overdraft).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OpearationsLog>(entity =>
            {
                entity.HasKey(e => e.Logid);

                entity.Property(e => e.AccountId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OperationDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.OpearationsLogs)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_OpearationsLogs_Accounts");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OperationAmount).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operations_Accounts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}