using System;

namespace OnlineBanking.Domain
{
    /// <summary>
    /// Entity for Account
    /// </summary>
    public class Account: EntityBase
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int BankId { get; set; }
        public int AccountNumber { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public override bool Validate()
        {
            var isValid = true;
            if (AccountNumber! > 0)
                isValid = false;

            isValid = true;
            return isValid;
        }
    }

    
}