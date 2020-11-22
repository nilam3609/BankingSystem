using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineBanking.Domain.Dto;
using OnlineBanking.Repository;
using OnlineBanking.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace OnlineBanking.UnitTest
{
    [TestClass]
    public class AccountServiceUnitTest
    {
        private IAccountRepository _accountRepo;

        [TestMethod]
        [Description("Given amount and accountId is passed When DepositAmount is called Then Balance for that accountId is updated")]
        public async Task DepositAmount_ToAccountId_ShouldUpdate_Balance()
        {
            //Arrange
            Mock<IAccountRepository> sut = new Mock<IAccountRepository>();
            _accountRepo = sut.Object;
            var depositRequest = new DepositRequestDto
            {
                AccountId = 1,
                Amount = 10
            };
            var expected = 20;
            sut.Setup(x => x.DepositAmount(depositRequest)).Returns(async () => expected);
            var accountService = new AccountService(_accountRepo);

            //Act
            var res = await accountService.DepositAmount(depositRequest);

            //Act
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [Description("Given amount and accountId is passed When WithdrawAmount for savingsaccount is called Then Balance for that accountId is updated")]
        public async Task WithdrawAmount_FromSavingsAccount_ShouldUpdate_Balance()
        {
            //Arrange
            Mock<IAccountRepository> sut = new Mock<IAccountRepository>();
            _accountRepo = sut.Object;
            var withdrawAmountRequest = new WithdrawAmountRequestDto
            {
                AccountId = 1,
                Amount = 20
            };
            var expected = 10;
            var savingsAccountSetting = new AccountTypeDetailDto
            {
                AccountId = 1,
                AnnualInterestRate = 2, //2% interest rate
                AccountType = 1, //Savings account
                DepositPeriodInDays = 0,
                InterestPayingFrequency = 0
            };
            sut.Setup(x => x.GetAccountSetting(1)).Returns(async () => savingsAccountSetting);
            sut.Setup(x => x.WithdrawAmount(withdrawAmountRequest)).Returns(async () => expected);
            var accountService = new AccountService(_accountRepo);

            //Act
            var res = await accountService.WithdrawAmount(withdrawAmountRequest);

            //Act
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [Description("Given amount and accountId is passed When Withdraw amount is called And Deposit account is not mature Then throw exception")]
        public async Task WithdrawAmount_FromDepositAccount_WhenAccountIsNotMatured_ShouldThrowException()
        {
            //Arrange
            Mock<IAccountRepository> sut = new Mock<IAccountRepository>();
            _accountRepo = sut.Object;
            var withdrawAmountRequest = new WithdrawAmountRequestDto
            {
                AccountId = 1,
                Amount = 20
            };
            var savingsAccountSetting = new AccountTypeDetailDto
            {
                AccountId = 1,
                AnnualInterestRate = 2, //2% interest rate
                AccountType = 2, //Deposit account
                DepositPeriodInDays = 30,
                InterestPayingFrequency = 1, //Daily,
                AccountCreatedDate = DateTime.Now.AddDays(-2)
            };
            sut.Setup(x => x.GetAccountSetting(1)).Returns(async () => savingsAccountSetting);
            var accountService = new AccountService(_accountRepo);

            //Act and Assert
            var ex = Assert.ThrowsAsync<Exception>(() => accountService.WithdrawAmount(withdrawAmountRequest));
            Assert.AreEqual("Deposit hasn't reached maturity", ex.Message);
        }

        [TestMethod]
        [Description("Given Scheduler is running When Scheduler calls DepositInterest Then balance should be updated and return true")]
        public async Task DepositInterest_WhenSchedulerRuns_Should_UpdateBalance_For_EachAccount()
        {
            //Arrange
            Mock<IAccountRepository> sut = new Mock<IAccountRepository>();
            
            var listOfAccount = new List<AccountTypeDetailDto>() {
                new AccountTypeDetailDto {  AccountId = 1, InterestPayingFrequency= 1 , AccountType = 2, AnnualInterestRate= 1, DepositPeriodInDays = 30,AccountCreatedDate = DateTime.Now.AddDays(-30)},
                new AccountTypeDetailDto{ AccountId = 2, InterestPayingFrequency= 2,AccountType = 2,AnnualInterestRate= 4,DepositPeriodInDays = 180,AccountCreatedDate = DateTime.Now.AddDays(-180)},
                new AccountTypeDetailDto{ AccountId = 3, InterestPayingFrequency= 3,AccountType = 2,AnnualInterestRate= 5,DepositPeriodInDays = 360,AccountCreatedDate = DateTime.Now.AddDays(-360)}
            };
            var expected = true;
            var interestForAccounts = new List<InterestAmountDto>()
            {
                new InterestAmountDto { AccountId = 1,InterestRate = 1},
                new InterestAmountDto { AccountId =2,InterestRate = 0.6667}
            };

            sut.Setup(x => x.GetAllAccounts()).Returns(async () => listOfAccount);
            sut.Setup(x => x.UpdateInterestAmount(interestForAccounts));
            _accountRepo = sut.Object;
            var accountService = new AccountService(_accountRepo);

            //Act
            var res = await accountService.DepositInterest();

            //Assert
            Assert.AreEqual(true, res);
        }
    }
}