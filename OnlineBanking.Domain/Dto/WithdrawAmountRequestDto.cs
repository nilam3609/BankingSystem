namespace OnlineBanking.Domain.Dto
{
    public class WithdrawAmountRequestDto
    {
        public int AccountId { get; set; }
        public float Amount { get; set; }
    }
}