using MISA.Core.Atribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entity
{
    public class Customer
    {
        #region Property

        [MISARequire("Id khách hàng")]
        public Guid CustomerId { get; set; }

        [MISARequire("Mã khách hàng")]
        public string CustomerCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MISARequire("Họ tên đầy đủ khách hàng")]
        public string FullName { get; set; }

        public int? Gender { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }
        [MISARequire("Email của khách hàng")]
        public string Email { get; set; }
        [MISARequire("Số điện thoại của khách hàng")]
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
