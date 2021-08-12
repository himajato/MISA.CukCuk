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
using System.Text.RegularExpressions;

namespace MISA.CukCuk.api.Controllers
{ 
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Api Lấy tất cả dữ liệu của tất cả khách hàng trong cơ sở dữ liệu
        /// created by: NHNGHIA (05/08/2021)
        /// </summary>
        /// <returns>Thông tin của tất cả khách hàng</returns>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            try 
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

                //Trả về cho client
                if(customer.Count() > 0)
                {
                    var respone = StatusCode(200,customer);
                    return respone;
                }
                else
                {
                    return StatusCode(204);
                }  
               
            }
            catch(Exception ex) 
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }

        /// <summary>
        /// Lấy thông tin của một khách hàng theo Id của họ
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Thông tin của khách hàng</returns>
        [HttpGet("{customerId}")]
        public IActionResult GetById(Guid customerId)
        {
            try
            {
                //Truy cập vào database MF947_NHNGHIA_CukCuk
                //1.Khai báo thông tin kết nối database: 
                var connectionString = "Host = 47.241.69.179;"
                                       + "Database = MF947_NHNGHIA_CukCuk;"
                                       + "User Id = dev;"
                                       + "Password = 12345678";

                //2. Khởi tạo đối tượng kết nối với db CustomerId = @CustomerIdParam
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                //3. Lấy dữ liệu:
                var sqlCommand = "SELECT * FROM Customer WHERE CustomerId = @CustomerIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerIdParam", customerId);

                var customer = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand, param: parameters);

                //4. Trả về cho client
                if(customer == null)
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISANoContent_204,
                        userMsg = Properties.Resources.MISANoContent_204,
                        errorCode = "Misa002",
                        moreInfor = "google.com"
                    };
                    return StatusCode(204, obj);
                } else
                {
                    var respone = StatusCode(200, customer);
                    return respone;
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }

        /// <summary>
        /// Thêm mới một khách hàng
        /// </summary>
        /// <param name="customer">Một file dạng JSON chứa thông tin khách hàng</param>
        /// <returns>Số hàng bị thay đổi</returns>
        [HttpPost]
        public IActionResult PostCustomers([FromBody]Customer customer)
        {
            try
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

                //3.1 Validate dữ liệu
                //Với email
                var validate = Regex.IsMatch(customer.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (validate == false)
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISABadrequest_400_Email,
                        userMsg = Properties.Resources.MISABadrequest_400_Email,
                        errorCode = "Misa001",
                        moreInfor = "google.com"
                    };
                    return StatusCode(400, obj);
                }

                //Với CustomerCode
                //Kiểm tra rỗng
                if(customer.CustomerCode == "")
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
                        userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
                        errorCode = "Misa001",
                        moreInfor = "google.com"
                    };
                    return StatusCode(400, obj);
                }

                //Kiểm tra trùng
                var sqlComman1 = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                var check = dbConnection.QueryFirstOrDefault(sqlComman1, param: parameters);
                if (check != null)
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                        userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                        errorCode = "Misa001",
                        moreInfor = "google.com"
                    };
                    return StatusCode(400, obj);
                }

                var sqlComman = $"INSERT INTO Customer({columsName}) VALUES({columsParam})";

                var rowEffect = dbConnection.Execute(sqlComman, param: parameters);

                if(rowEffect != 0)
                {
                    return StatusCode(201);
                }
                return StatusCode(200, rowEffect);
            }
            catch(Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }


        /// <summary>
        /// Xóa thông tin một khách hàng theo Id của họ
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Thông tin của khách hàng bị xóa</returns>
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomersById(Guid customerId)
        {
            try
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
                parameters.Add("@CustomerIdParam", customerId);

                var rowEffect = dbConnection.Execute(sqlCommand, param: parameters);

                //4. Trả về cho client
                if(rowEffect > 0)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    return StatusCode(204);
                }
              
            }
            catch(Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }

        /// <summary>
        /// Sửa thông tin của một khách hàng theo Id
        /// </summary>
        /// <param name="customerId">ID của khách hàng muốn sửa</param>
        /// <returns>Sửa thông tin thành công</returns>
        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomerById(Guid customerId, [FromBody]Customer customer)
        {
            try
            {
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

                    // columsName += $"{propName},";
                    columsParam += $"{propName}=@{propName},";
                }

                //3.1 Validate dữ liệu
                //Với email
                var validate = Regex.IsMatch(customer.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (validate == false)
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISABadrequest_400_Email,
                        userMsg = Properties.Resources.MISABadrequest_400_Email,
                        errorCode = "Misa001",
                        moreInfor = "google.com"
                    };
                    return StatusCode(400, obj);
                }

                //Với CustomerCode
                //Kiểm tra rỗng
                if (customer.CustomerCode == "")
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
                        userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
                        errorCode = "Misa001",
                        moreInfor = "google.com"
                    };
                    return StatusCode(400, obj);
                }

                //Kiểm tra trùng
                var sqlComman1 = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                var check = dbConnection.QueryFirstOrDefault(sqlComman1, param: parameters);
                if (check != null)
                {
                    var obj = new
                    {
                        devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                        userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                        errorCode = "Misa001",
                        moreInfor = "google.com"
                    };
                    return StatusCode(400, obj);
                }

                // columsName = columsName.Remove(columsName.Length - 1, 1); 
                columsParam = columsParam.Remove(columsParam.Length - 1, 1);

                var sqlComman = $"UPDATE Customer SET {columsParam} WHERE CustomerId=@CustomerId";

                var rowEffect = dbConnection.Execute(sqlComman, param: parameters);
                if(rowEffect > 0)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch(Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }
    }
}
