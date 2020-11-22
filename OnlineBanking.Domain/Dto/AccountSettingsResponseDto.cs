using OnlineBanking.Domain.Enum;
using System.Collections.Generic;

namespace OnlineBanking.Domain.Dto
{
    public class AccountSettingsDto
    {
        public int Id { get; set; }
        public int AccountType { get; set; }
        public int AnnualInterestRate { get; set; }
        public int DepositPeriodInDays { get; set; }
    }

    public class AccountCreateDataDto
    {
        public List<AccountSettingsDto> AccountSettings { get; set; }
        public List<DepositFrequencyDto> DepositFrequency { get; set; }
    }

    public class DepositFrequencyDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}