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

        public string CustomerCode { get;set }

        public string FirstName { get;set }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int? Gender { get; set; }

        public string Email { get; set; }
        #endregion
    }
}
