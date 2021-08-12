using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Modal
{
    public class Customer
    {
        #region Property
        public Guid CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int? Gender { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Guid CustomerGroupId { get; set; }

        public float DebitAmount { get; set; }

        public string MemberCardCode { get; set; }

        public string CompanyName { get; set; }

        public string CompanyTaxCode { get; set; }

        public bool IsStopFollow { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        #endregion
    }
}
