using OnlineBanking.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBanking.Repository
{
    public interface IAccountRepository
    {
        Task<AccountResponseDto> AddClientAccount(AccountCreationRequestDto accountCreationRequestDto, int accountNumber);

        Task<float> DepositAmount(DepositRequestDto depositRequestDto);

        Task<float> WithdrawAmount(WithdrawAmountRequestDto withdrawAmountRequestDto);

        Task<AccountTypeDetailDto> GetAccountSetting(int accountId);

        Task<float> GetBalance(int accountId);

        Task<List<AccountTypeDetailDto>> GetAllAccounts();
        void UpdateInterestAmount(List<InterestAmountDto> interestAmountDto);
        Task<List<AccountSettingsDto>> GetInterestRatesAndPeriod();
        Task<List<ClientAccountsResponseDto>> GetClientAccounts(int clientId);
    }
}