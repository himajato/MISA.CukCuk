using MISA.Core.Atribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entity
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        
        [MISARequire("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MISARequire("Họ tên đầy đủ nhân viên")]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }

        [MISARequire("Email của nhân viên")]
        public string Email { get; set; }

        [MISARequire("Số điện thoại của nhân viên")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string WorkStatus { get; set; }
        public string PersonalTaxCode { get; set; }
        public double? Salary { get; set; }
    }
}
