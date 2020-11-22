namespace OnlineBanking.Domain.Dto
{
    public class DepositRequestDto
    {
        public int AccountId { get; set; }
        public float Amount { get; set; }
    }
}