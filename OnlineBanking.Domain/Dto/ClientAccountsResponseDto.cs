namespace OnlineBanking.Domain.Dto
{
    public class ClientAccountsResponseDto
    {
        public int AccountNumber { get; set; }
        public float Balance { get; set; }
        public AccountTypeDetailDto AccountType { get; set; }
    }
}