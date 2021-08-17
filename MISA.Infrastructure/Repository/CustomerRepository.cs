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
    public class CustomerRepository : ICustomerRepository
    {
        public int AddCustomer(Customer customer)
        {
            //// Khởi tạo ID mới cho nhân viên
            customer.CustomerId = Guid.NewGuid();

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Khai báo dynamicParam: 
            DynamicParameters parameters = new DynamicParameters();

            //3. Lấy prop name và prop type: 
            var columsName = string.Empty;
            var columsParam = string.Empty;

            var props = customer.GetType().GetProperties();

            //Duyệt từng properties
            foreach (var prop in props)
            {
                //Lấy tên của prop
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(customer);

                //Lấy kiểu dữ liệu
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi propName của đối tượng
                parameters.Add($"@{propName}", propValue);
                columsName += $"{propName},";
                columsParam += $"@{propName},";
            }

            columsName = columsName.Remove(columsName.Length - 1, 1);
            columsParam = columsParam.Remove(columsParam.Length - 1, 1);

            var sqlComman = $"INSERT INTO Customer({columsName}) VALUES({columsParam})";

            var rowEffect = dbConnection.Execute(sqlComman, param: parameters);

            return rowEffect;
        }

        public int DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public List<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public int UpdateCustomer(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
