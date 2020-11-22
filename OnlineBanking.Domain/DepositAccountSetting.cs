using System;

namespace OnlineBanking.Domain
{
    /// <summary>
    /// Entity for Deposit-Saving accounts details -e.g DepositAccount- Days- Frequency- AccountId(Foreign)
    /// </summary>
    public class DepositAccountSetting
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int AnnualInterestId { get; set; }
        public int InterestPayingFrequency { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}