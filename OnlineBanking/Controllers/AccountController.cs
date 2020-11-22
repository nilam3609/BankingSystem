using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBanking.Domain.Dto;
using OnlineBanking.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBanking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<ClientController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost("AddClientAccount")]
        public async Task<AccountResponseDto> AddClientAccount(AccountCreationRequestDto accountCreationRequestDto)
        {
            var account = await _accountService.AddClientAccount(accountCreationRequestDto);
            return account;
        }

        [HttpPut("DepositAmount")]
        public async Task<float> DepositAmount(DepositRequestDto depositRequestDto)
        {
            var balance = await _accountService.DepositAmount(depositRequestDto);
            return balance;
        }

        [HttpPut("WithdrawAmount")]
        public async Task<float> WithdrawAmount(WithdrawAmountRequestDto withdrawAmountRequestDto)
        {
            var balance = await _accountService.WithdrawAmount(withdrawAmountRequestDto);
            return balance;
        }

        [HttpPut("GetBalance")]
        public async Task<float> GetBalance([FromBody] int accountId)
        {
            var balance = await _accountService.GetBalance(accountId);
            return balance;
        }

        [HttpGet("GetInterestRatesAndPeriod")]
        public async Task<AccountCreateDataDto> GetInterestRatesAndPeriod()
        {
            var settings = await _accountService.GetInterestRatesAndPeriod();
            return settings;
        }

        [HttpGet("GetAccounts")]
        public async Task<List<ClientAccountsResponseDto>> GetAccounts(int id)
        {
            var settings = await _accountService.GetClientAccounts(id);
            return settings;
        }
    }
}