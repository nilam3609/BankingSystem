using OnlineBanking.Common;
using System;

namespace OnlineBanking.Domain
{
    /// <summary>
    /// Entity for Bank
    /// </summary>
    public class Bank
    {
        public int Id { get; set; }
        public string Name;
        public string _name 
        {
            get
            {
                 Name.InsertSpaceExtentedMethod();
                return StringHandler.InsertSpace(Name);
            }
            set { Name = value; } 
        }
        public int Code { get; set; }
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