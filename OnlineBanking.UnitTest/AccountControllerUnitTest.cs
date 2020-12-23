using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineBanking.Common;
using OnlineBanking.Controllers;
using OnlineBanking.Repository.Interface;
using System.Threading.Tasks;

namespace OnlineBanking.UnitTest
{
    [TestClass]
    public class AccountControllerUnitTest
    {
        [TestMethod]
        public async Task AccountControllerTest()
        {
            var accountObj = new AccountController();
            var accountNameId = accountObj.GetAccountNameAndId();
            var account = accountObj.AccountName;
        }

        [TestMethod]
        public async Task TestExtensionMethod()
        {
            var name = "Bank";
            var expectedName = "Bank ";

            var d = name.InsertSpaceExtentedMethod();
            var assert = Assert.Equals(d, expectedName);
        }

        [TestMethod]
        public async Task TestUnitOfWork()
        {
            var unitOfworkMock = new Mock<IUnitOfWork>();
            unitOfworkMock.Setup(u => u.Accounts.GetById(1)).Returns(new Domain.Account {AccountNumber = 123 });
            var data = new AccountController(unitOfworkMock.Object);
            var account = data.GetAccount();

        }


    }
}