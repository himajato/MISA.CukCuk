using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.CukCuk.api.Modal;
using System.Data;
using MySqlConnector;
using Dapper;

namespace MISA.CukCuk.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCustomers()
        {
            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu:
            var sqlCommand = "SELECT * FROM Customer";
            var customer = dbConnection.Query<Customer>(sqlCommand);

            //4. Trả về cho client
            var respone = StatusCode(200, customer);
            return respone;
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomersById(Guid employeeId)
        {
            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu:
            var sqlCommand = "SELECT * FROM Customer WHERE CustomerId = @CustomerIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerIdParam", employeeId);

            var customer = dbConnection.Query<Customer>(sqlCommand, param: parameters);

            //4. Trả về cho client
            var respone = StatusCode(200, customer);
            return respone;
        }

        [HttpPost]
        public IActionResult PostCustomers(Customer customer)
        {
            // Khởi tạo ID mới cho nhân viên
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
            foreach(var prop in props)
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

            columsName.Remove(columsName.Length - 1, 1);
            columsParam.Remove(columsParam.Length - 1, 1);

            var sqlComman = $"INSERT INTO Customer({columsName}) VALUES(@{columsParam})";

            var rowEffect = dbConnection.Execute(sqlComman, param: parameters);
            return StatusCode(200, rowEffect);
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomersById(Guid employeeId)
        {
            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu:
            var sqlCommand = "DELETE FROM Customer WHERE CustomerId = @CustomerIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerIdParam", employeeId);

            var customer = dbConnection.Query<Customer>(sqlCommand, param: parameters);

            //4. Trả về cho client
            var respone = StatusCode(200, customer);
            return respone;
        }
    }
}
