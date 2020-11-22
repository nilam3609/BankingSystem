using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineBanking.Domain;
using OnlineBanking.Domain.Dto;
using OnlineBanking.Repository;
using System;
using System.Threading.Tasks;

namespace OnlineBanking.UnitTest
{
    [TestClass]
    public class AccountRepoUnitTest
    {
        [TestMethod]
        [Description("Given amount and accountId is passed When DepositAmount is called Then Balance for that accountId is updated in db")]
        public async Task DepositAmout_ToAccountId_ShouldUpdate_Balance()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                            .UseInMemoryDatabase(databaseName: "OBS")
                            .Options;

            using (var context = new BankContext(options))
            {
                context.Accounts.Add(new Account { Id = 1, AccountNumber = 222616, Balance = 10050, ClientId = 1, BankId = 1, CreatedDate = DateTime.Now });
                context.SaveChanges();
            }
            var mockMapper = new Mock<IMapper>();
            var dto = new DepositRequestDto
            {
                AccountId = 1,
                Amount = 10
            };

            using (var context = new BankContext(options))
            {
                AccountRepository accountRepository = new AccountRepository(context, mockMapper.Object);
                var amt = await accountRepository.DepositAmount(dto);
                Assert.AreEqual(10060, amt);
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        [Description("Given amount and accountId is passed When WithdrawAmount is called Then Balance for that accountId is updated")]
        public async Task WithdrawAmount_FromAccountId_ShouldUpdate_Balance()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                            .UseInMemoryDatabase(databaseName: "OBS")
                            .Options;

            using (var context = new BankContext(options))
            {
                context.Accounts.Add(new Account { Id = 1, AccountNumber = 222616, Balance = 10050, ClientId = 1, BankId = 1, CreatedDate = DateTime.Now });
                context.SaveChanges();
            }
            var mockMapper = new Mock<IMapper>();
            var dto = new WithdrawAmountRequestDto
            {
                AccountId = 1,
                Amount = 10
            };

            using (var context = new BankContext(options))
            {
                AccountRepository accountRepository = new AccountRepository(context, mockMapper.Object);
                var amt = await accountRepository.WithdrawAmount(dto);
                Assert.AreEqual(10040, amt);
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        [Description("Given Accountid is passed when GetBalance is called Then it returns correct Balance")]
        public async Task GetBalance_FromAccountId_Should_Return_Balance()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                            .UseInMemoryDatabase(databaseName: "OBS")
                            .Options;

            using (var context = new BankContext(options))
            {
                context.Accounts.Add(new Account { Id = 1, AccountNumber = 222616, Balance = 10050, ClientId = 1, BankId = 1, CreatedDate = DateTime.Now });
                context.SaveChanges();
            }
            var mockMapper = new Mock<IMapper>();

            using (var context = new BankContext(options))
            {
                AccountRepository accountRepository = new AccountRepository(context, mockMapper.Object);
                var amt = await accountRepository.GetBalance(1);
                Assert.AreEqual(10050, amt);
                context.Database.EnsureDeleted();
            }
        }
    }
}