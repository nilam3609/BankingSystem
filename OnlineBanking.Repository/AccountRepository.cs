using AutoMapper;
using OnlineBanking.Domain;
using OnlineBanking.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Repository
{
    /// <summary>
    /// Account Repository
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _bankContext;
        private readonly IMapper _mapper;

        public AccountRepository(BankContext bankContext, IMapper mapper)
        {
            _bankContext = bankContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new client's account 
        /// </summary>
        /// <param name="accountCreationRequestDto"></param>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public async Task<AccountResponseDto> AddClientAccount(AccountCreationRequestDto accountCreationRequestDto, int accountNumber)
        {
            var accountEntity = _mapper.Map<Account>(accountCreationRequestDto);
            accountEntity.AccountNumber = accountNumber;
            accountEntity.CreatedDate = DateTime.Now;
            _bankContext.Add(accountEntity);
            _bankContext.SaveChanges();

            var depositAccountSetting = _mapper.Map<DepositAccountSetting>(accountCreationRequestDto);
            depositAccountSetting.AccountId = accountEntity.Id;
            depositAccountSetting.CreatedDate = DateTime.Now;
            _bankContext.Add(depositAccountSetting);
            _bankContext.SaveChanges();

            return new AccountResponseDto
            {
                AccountNumber = accountEntity.AccountNumber,
                Balance = accountEntity.Balance,
            };
        }

        /// <summary>
        /// Deposit amount to client's account and return final balance
        /// </summary>
        /// <param name="depositRequestDto"></param>
        /// <returns></returns>
        public async Task<float> DepositAmount(DepositRequestDto depositRequestDto)
        {
            var account = _bankContext.Accounts.FirstOrDefault(x => x.Id == depositRequestDto.AccountId);
            if (account != null)
            {
                account.Balance += depositRequestDto.Amount;
            }
            _bankContext.SaveChanges();
            return account.Balance;
        }

        /// <summary>
        /// Withdraw amount from given accountId and return final balance
        /// </summary>
        /// <param name="withdrawAmountRequestDto"></param>
        /// <returns></returns>
        public async Task<float> WithdrawAmount(WithdrawAmountRequestDto withdrawAmountRequestDto)
        {
            //withdraw amount from account
            var account = _bankContext.Accounts.FirstOrDefault(x => x.Id == withdrawAmountRequestDto.AccountId);
            if (account != null && account.Balance >= withdrawAmountRequestDto.Amount)
            {
                account.Balance -= withdrawAmountRequestDto.Amount;
                _bankContext.SaveChanges();
                return account.Balance;
            }
            //exception if requested withdraw is more than available balance
            throw new Exception("Insufficient Balance");
            
        }
        /// <summary>
        /// Get account setting like type of account, deposit period, freq of interest deposit and annual interest rate
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>

        public async Task<AccountTypeDetailDto> GetAccountSetting(int accountId)
        {
            var account = _bankContext.DepositAccountSettings.FirstOrDefault(x => x.AccountId == accountId);
            if (account != null)
            {
                var annualData = _bankContext.AnnualInterests.FirstOrDefault(x => x.Id == account.AnnualInterestId);
                if (annualData != null)
                {
                    return new AccountTypeDetailDto
                    {
                        AccountType = annualData.AccountType,
                        AnnualInterestRate = annualData.AnnualInterestRate,
                        DepositPeriodInDays = annualData.DepositPeriodInDays,
                        InterestPayingFrequency = account.InterestPayingFrequency,
                        AccountCreatedDate = account.CreatedDate,
                        AccountId = account.AccountId
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Get balance for given balance
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<float> GetBalance(int accountId)
        {
            var account = _bankContext.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (account != null)
            {
                return account.Balance;
            }
            throw new Exception("Account dosn't exist");
        }

        /// <summary>
        /// Get all accounts linked to any client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<List<ClientAccountsResponseDto>> GetClientAccounts(int clientId)
        {
            var accountList = new List<ClientAccountsResponseDto>();
            var accounts = _bankContext.Accounts.Where(x => x.ClientId == clientId).ToList();
            if (accounts != null)
            {
                foreach (var item in accounts)
                {
                    var account = new ClientAccountsResponseDto
                    {
                        AccountType = await GetAccountSetting(item.Id),
                        Balance = item.Balance,
                        AccountNumber = item.AccountNumber
                    };
                    accountList.Add(account);
                }
                return accountList;
            }
            throw new Exception("Accounts don't exist");
        }

        /// <summary>
        /// Get all accounts for bank to run schedule to deposit interest amount
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountTypeDetailDto>> GetAllAccounts()
        {
            return (from d in _bankContext.DepositAccountSettings
                    join a in _bankContext.AnnualInterests
                    on d.AnnualInterestId equals a.Id
                    select new AccountTypeDetailDto
                    {
                        AccountType = a.AccountType,
                        AnnualInterestRate = a.AnnualInterestRate,
                        DepositPeriodInDays = a.DepositPeriodInDays,
                        InterestPayingFrequency = d.InterestPayingFrequency,
                        AccountCreatedDate = d.CreatedDate,
                        AccountId = d.AccountId
                    }).ToList();
        }

        /// <summary>
        /// Update interest to bank accounts after scheduler run and interest calculation
        /// </summary>
        /// <param name="interestAmountDto"></param>
        /// <returns></returns>
        public void UpdateInterestAmount(List<InterestAmountDto> interestAmountDto)
        {
            foreach (var acc in interestAmountDto)
            {
                var account = _bankContext.Accounts.FirstOrDefault(x => x.Id == acc.AccountId);
                if (account != null)
                {
                    account.Balance += (float)(account.Balance * acc.InterestRate);
                }
            }
            _bankContext.SaveChanges();
        }

        /// <summary>
        /// Get interest rates and period from master table
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountSettingsDto>> GetInterestRatesAndPeriod()
        {
            var setting = _bankContext.AnnualInterests.ToList();
            return _mapper.Map<List<AccountSettingsDto>>(setting);
        }
    }
}