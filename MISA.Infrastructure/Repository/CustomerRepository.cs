using Dapper;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {


        public int AddCustomer(Customer customer)
        {
            return base.Add<Customer>(customer);
        }

        public int DeleteCustomer(Guid customerId)
        {
            return base.Delete<Customer>(customerId);
        }

        public List<Customer> Get()
        {
            return base.GetAll<Customer>();
        }

        public object GetById(Guid customerId)
        {
           return base.GetById<Customer>(customerId);
        }

        public int UpdateCustomer(Customer customer, Guid customerId)
        {
            return base.Update<Customer>(customer, customerId);
        }
    }
}
