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
            //Truy cập vào database
            //1. Khai báo thông tin kết nối database: 
            var connectionString = "Host = localhost;"
                                   + "Database = MISACukCuk"
                                   + "User Id = dev;"
                                   + "Password = 123456789";

            //2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu:
            var sqlCommand = "SELECT * FROM Customer";
            var customer = dbConnection.Query<Customer>(sqlCommand);

            //4. Trả về cho client
            var respone = StatusCode(200,customer);
            return respone;
        }
    }
}
