using OnlineBanking.Domain.Dto;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Repository;
using OnlineBanking.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService()
        {

        }

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Create new account for client
        /// </summary>
        /// <param name="accountCreationRequestDto"></param>
        /// <returns></returns>
        public async Task<AccountResponseDto> AddClientAccount(AccountCreationRequestDto accountCreationRequestDto)
        {
            var accountNumber = GenerateAccountNumber();
            return await _accountRepository.AddClientAccount(accountCreationRequestDto, accountNumber);
        }

        private int GenerateAccountNumber()
        {
            Random generator = new Random();
            return generator.Next(0, 999999999);
        }

        /// <summary>
        /// Deposit amount into clients account
        /// </summary>
        /// <param name="depositRequestDto"></param>
        /// <returns></returns>
        public async Task<float> DepositAmount(DepositRequestDto depositRequestDto)
        {
            return await _accountRepository.DepositAmount(depositRequestDto);
        }

        /// <summary>
        /// Withdraw amount for account
        /// </summary>
        /// <param name="withdrawAmountRequestDto"></param>
        /// <returns></returns>
        public async Task<float> WithdrawAmount(WithdrawAmountRequestDto withdrawAmountRequestDto)
        {
            //check account type from account Id
            var account = await _accountRepository.GetAccountSetting(withdrawAmountRequestDto.AccountId);
            if (account != null && account.AccountType == (int)AccountType.SavingsAccount)
            {
                //allow withdraw if account type is savings account
                return await _accountRepository.WithdrawAmount(withdrawAmountRequestDto);
            }
            else if (account != null && account.AccountType == (int)AccountType.DepositAccount)
            {
                //get days diff- deposit period in days and created date for current account : Assuming Deposit is on the day of account creation
                var days = (DateTime.Now - account.AccountCreatedDate).Days;
                if (days >= account.DepositPeriodInDays)
                {
                    //allow withdrawing if maturity day has arrived for deposit account
                    return await _accountRepository.WithdrawAmount(withdrawAmountRequestDto);
                }
                else
                {
                    throw new Exception("Deposit hasn't reached maturity");
                }
            }
            throw new Exception("Account doesn't exist");
        }

        public async Task<float> GetBalance(int accountId)
        {
            return await _accountRepository.GetBalance(accountId);
        }

        /// <summary>
        /// This is supposed to be run by Scheduler in order to deposit Interest rates to client's account based on frequency
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DepositInterest()
        {
            var interestAmounts = new List<InterestAmountDto>();
            //Get all accounts for the bank
            var accounts = await _accountRepository.GetAllAccounts();
            foreach (var acc in accounts)
            {
                //Check if account is savings account
                if (acc.AccountType == (int)AccountType.SavingsAccount)
                {
                    // Check if today is end of month - allow adding interest to account if its end of month
                    if (DateTime.Now == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)))
                    {
                        interestAmounts.Add(
                            new InterestAmountDto
                            {
                                AccountId = acc.AccountId,
                                InterestRate = acc.AnnualInterestRate / 12
                            });
                    }
                }
                //Check if account is Deposit account
                else if (acc.AccountType == (int)AccountType.DepositAccount)
                {
                    //Check if frquency is Daily
                    if (acc.InterestPayingFrequency == (int)InterestPayingFrequency.Daily)
                    {
                        //Allow only if Deposit has not reached maturity
                        if ((DateTime.Now - acc.AccountCreatedDate).Days <= acc.DepositPeriodInDays)
                        {
                            interestAmounts.Add(
                            new InterestAmountDto
                            {
                                AccountId = acc.AccountId,
                                InterestRate = acc.AnnualInterestRate / acc.DepositPeriodInDays //Calculate interest rate
                            });
                        }
                    }
                    else if (acc.InterestPayingFrequency == (int)InterestPayingFrequency.Monthly)
                    {
                        //If freq is monthly, check is deposit hsa not reached maturity and its end of month to add interest rate
                        if ((DateTime.Now - acc.AccountCreatedDate).Days <= acc.DepositPeriodInDays
                            && DateTime.Now == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)))
                        {
                            interestAmounts.Add(
                            new InterestAmountDto
                            {
                                AccountId = acc.AccountId,
                                InterestRate = acc.AnnualInterestRate / (acc.DepositPeriodInDays / 30)
                            });
                        }
                    }
                    else if (acc.InterestPayingFrequency == (int)InterestPayingFrequency.Quarterly)
                    {
                        //Get all the days of quareter months 
                        var quarters = new List<DateTime>()
                         {
                            new DateTime(DateTime.Now.Year, 3, DateTime.DaysInMonth(DateTime.Now.Year, 3)),
                            new DateTime(DateTime.Now.Year, 6, DateTime.DaysInMonth(DateTime.Now.Year, 6)),
                            new DateTime(DateTime.Now.Year, 9, DateTime.DaysInMonth(DateTime.Now.Year, 9)),
                            new DateTime(DateTime.Now.Year, 12, DateTime.DaysInMonth(DateTime.Now.Year, 12))
                         };
                        //check if deposit has not matured and its day of the quarter 
                        if ((DateTime.Now - acc.AccountCreatedDate).Days <= acc.DepositPeriodInDays
                            && quarters.Any(x => x == DateTime.Now))
                        {
                            interestAmounts.Add(
                            new InterestAmountDto
                            {
                                AccountId = acc.AccountId,
                                InterestRate = acc.AnnualInterestRate / (acc.DepositPeriodInDays / 180)
                            });
                        }
                    }
                    else if (acc.InterestPayingFrequency == (int)InterestPayingFrequency.Annually)
                    {
                        if ((DateTime.Now - acc.AccountCreatedDate).Days == acc.DepositPeriodInDays)
                        {
                            interestAmounts.Add(
                            new InterestAmountDto
                            {
                                AccountId = acc.AccountId,
                                InterestRate = acc.AnnualInterestRate / (acc.DepositPeriodInDays / 360)
                            });
                        }
                    }
                    //Check if instrest deposit on day of maturity
                    else if (acc.InterestPayingFrequency == (int)InterestPayingFrequency.OnMaturity)
                    {
                        if ((DateTime.Now - acc.AccountCreatedDate).Days == acc.DepositPeriodInDays)
                        {
                            interestAmounts.Add(
                            new InterestAmountDto
                            {
                                AccountId = acc.AccountId,
                                InterestRate = acc.AnnualInterestRate //direct interest rate to the balance
                            });
                        }
                    }
                }
            }
             _accountRepository.UpdateInterestAmount(interestAmounts);
            return true;
        }

        /// <summary>
        /// Get interest rate and period from master table
        /// </summary>
        /// <returns></returns>
        public async Task<AccountCreateDataDto> GetInterestRatesAndPeriod()
        {
            var model = new AccountCreateDataDto();
            model.AccountSettings = await _accountRepository.GetInterestRatesAndPeriod();

            model.DepositFrequency = Enum.GetValues(typeof(InterestPayingFrequency))
                                      .Cast<InterestPayingFrequency>()
                                      .Select(v => new DepositFrequencyDto
                                      {
                                          Value = v.ToString(),
                                          Id = (int)v
                                      })
                                      .ToList();
            return model;
        }

        /// <summary>
        /// Get all accounts linked to the client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<List<ClientAccountsResponseDto>> GetClientAccounts(int clientId)
        {
            return await _accountRepository.GetClientAccounts(clientId);
        }
    }
}