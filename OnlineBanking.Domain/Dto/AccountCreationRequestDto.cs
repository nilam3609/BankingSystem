namespace OnlineBanking.Domain.Dto
{
    public class AccountCreationRequestDto
    {
        public int ClientId { get; set; }
        public int BankId { get; set; }
        public int AnnualInterestId { get; set; }
        public int InterestPayingFrequency { get; set; }
    }
}