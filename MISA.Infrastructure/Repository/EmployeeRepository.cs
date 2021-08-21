using Microsoft.Extensions.Configuration;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    class EmployeeRepository :IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) 
        {
        }

        public int Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public bool CheckCodeDuplicate(string entityCode)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public object GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Update(Employee entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
