using OnlineBanking.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBanking.Service.Interface
{
    public interface IAccountService
    {
        Task<AccountResponseDto> AddClientAccount(AccountCreationRequestDto accountCreationRequestDto);

        Task<float> DepositAmount(DepositRequestDto depositRequestDto);

        Task<float> WithdrawAmount(WithdrawAmountRequestDto withdrawAmountRequestDto);

        Task<float> GetBalance(int accountId);

        Task<bool> DepositInterest();

        Task<AccountCreateDataDto> GetInterestRatesAndPeriod();

        Task<List<ClientAccountsResponseDto>> GetClientAccounts(int clientId);
    }
}