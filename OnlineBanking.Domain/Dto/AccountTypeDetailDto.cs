using System;

namespace OnlineBanking.Domain.Dto
{
    public class AccountTypeDetailDto
    {
        public int AccountId { get; set; }
        public int AccountType { get; set; }
        public int AnnualInterestRate { get; set; }
        public int DepositPeriodInDays { get; set; }
        public int InterestPayingFrequency { get; set; }
        public DateTime AccountCreatedDate { get; set; }
    }
}
