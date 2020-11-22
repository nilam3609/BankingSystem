using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain;
using System;

namespace OnlineBanking.Repository
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<AnnualInterest> AnnualInterests { get; set; }
        public virtual DbSet<DepositAccountSetting> DepositAccountSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnualInterest>().HasData(
                new AnnualInterest() { Id = 1, AccountType = 1, AnnualInterestRate = 2, DepositPeriodInDays = 0 },
                new AnnualInterest() { Id = 2, AccountType = 2, AnnualInterestRate = 1, DepositPeriodInDays = 30 },
                new AnnualInterest() { Id = 3, AccountType = 2, AnnualInterestRate = 4, DepositPeriodInDays = 180 },
                new AnnualInterest() { Id = 4, AccountType = 2, AnnualInterestRate = 5, DepositPeriodInDays = 360 }
                );

            modelBuilder.Entity<Bank>().HasData(
              new Bank() { Id = 1, Name = "FourthLine", Code = 001, StreetAddress1 = "XYZ", City = "Amsterdam", Country = "Netherland", PostCode = "2766CC", CreatedDate = DateTime.Now }
              );
        }
    }
}