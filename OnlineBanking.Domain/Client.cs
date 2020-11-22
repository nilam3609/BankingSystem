using System;

namespace OnlineBanking.Domain
{
    /// <summary>
    /// Entity to create client
    /// </summary>
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string IsActive { get; set; }
        public string IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}