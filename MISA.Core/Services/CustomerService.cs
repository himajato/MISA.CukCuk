using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        //ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;
        public CustomerService(IBaseRepository baseRepository):base(baseRepository)
        {
            _serviceResult = new ServiceResult();
        }

        public ServiceResult Add(Customer customer)
        {
            return base.Add<Customer>(customer);
        }

        public ServiceResult GetAll()
        {
            return base.GetAll<Customer>();
        }

        public ServiceResult GetById(Guid customerId)
        {
            return base.GetById<Customer>(customerId);
        }

        public ServiceResult Update(Customer customer, Guid customerId)
        {
            return base.Update<Customer>(customer, customerId);
        }

        public bool CustomValidate(string customerCode)
        {
            return true;
        }
    }
}
