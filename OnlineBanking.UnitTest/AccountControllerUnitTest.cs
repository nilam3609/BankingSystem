using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineBanking.Common;
using OnlineBanking.Controllers;
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
    }
}