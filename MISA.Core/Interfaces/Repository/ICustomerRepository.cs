using MISA.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Customer> Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        object GetById(Guid customerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        int AddCustomer(Customer customer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        int UpdateCustomer(Customer customer, Guid customerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        int DeleteCustomer(Guid customerId);
    }
}
