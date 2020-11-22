namespace OnlineBanking.Domain
{
    /// <summary>
    /// Entity to store master data for all types
    /// </summary>
    public class AnnualInterest
    {
        public int Id { get; set; }
        public int AccountType { get; set; }
        public int AnnualInterestRate { get; set; }
        public int DepositPeriodInDays { get; set; }
    }
}