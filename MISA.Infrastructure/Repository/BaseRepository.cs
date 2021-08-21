using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        public readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {   
            _connectionString = configuration.GetConnectionString("MisaCukCukDatabase");
        }
        /// <summary>
        /// Thêm thực thể vào db
        /// </summary>
        /// <param name="entity">Thực thể</param>
        /// <returns></returns>
        public int Add(MISAEntity entity)
        {
            //// Khởi tạo ID mới cho nhân viên
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
           

            //2. Khởi tạo đối tượng kết nối với db
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo dynamicParam: 
                DynamicParameters parameters = new DynamicParameters();

                //3. Lấy prop name và prop type: 
                var columsName = string.Empty;
                var columsParam = string.Empty;

                var props = entity.GetType().GetProperties();

                //Duyệt từng properties
                foreach (var prop in props)
                {
                    //Lấy tên của prop
                    var propName = prop.Name;

                    //Lấy value của prop
                    var propValue = prop.GetValue(entity);
                    // Nếu là  Id thì tự sinh ra Id mới
                    if (prop.Name == $"{className}Id" && prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }

                    //Lấy kiểu dữ liệu
                    var propType = prop.PropertyType;

                    //Thêm param tương ứng với mỗi propName của đối tượng
                    parameters.Add($"@{propName}", propValue);
                    columsName += $"{propName},";
                    columsParam += $"@{propName},";
                }

                columsName = columsName.Remove(columsName.Length - 1, 1);
                columsParam = columsParam.Remove(columsParam.Length - 1, 1);

                var sqlComman = $"INSERT INTO {className}({columsName}) VALUES({columsParam})";

                var rowEffect = dbConnection.Execute(sqlComman, param: parameters);

                return rowEffect;
            }
        }

        /// <summary>
        /// Lấy hết dữ liệu trong db
        /// </summary>
        /// <returns>Danh sách thực thể trong db</returns>
        public List<MISAEntity> GetAll()
        {
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //Khai báo thông tin kết nối database: 
          
            // Khởi tạo đối tượng kết nối với db
            using(IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //3. Lấy dữ liệu: (thêm try catch)
                var sqlCommand = $"SELECT * FROM {className}";
                var entity = dbConnection.Query<MISAEntity>(sqlCommand);
                return (List<MISAEntity>)entity;
            }
        }
        
        /// <summary>
        /// Lấy thực thể theo Id 
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Thực thể có Id muốn lấy</returns>
        public object GetById(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //Khai báo thông tin kết nối database: 
            

            // Khởi tạo đối tượng kết nối với db CustomerId = @CustomerIdParam
            using(IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //3. Lấy dữ liệu:
                var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @{className}IdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}IdParam", entityId);

                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);

                return entity;
            }  
        }

        /// <summary>
        /// Sửa thông tin thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể muốn sửa</param>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>số hàng bị thay đổi: 1 - thành công, 0 - thất bại</returns>
        public int Update(MISAEntity entity, Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            //Truy cập vào database MF947_NHNGHIA_CukCuk
           
            //. Khởi tạo đối tượng kết nối với db
            //IDbConnection dbConnection = new MySqlConnection(connectionString);
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo dynamicParam: 
                DynamicParameters parameters = new DynamicParameters();


                // Lấy prop name và prop type: 
                //var columsName = string.Empty;
                var columsParam = string.Empty;

                var props = entity.GetType().GetProperties();

                //Duyệt từng properties
                foreach (var prop in props)
                {
                    //Lấy tên của prop
                    var propName = prop.Name;

                    //Lấy value của prop
                    var propValue = prop.GetValue(entity);

                    //Lấy kiểu dữ liệu
                    var propType = prop.PropertyType;

                    //Thêm param tương ứng với mỗi propName của đối tượng
                    parameters.Add($"@{propName}", propValue);

                    // columsName += $"{propName},";
                    columsParam += $"{propName}=@{propName},";
                }

                ////3.1 Validate dữ liệu
                ////Với email
                //var validate = Regex.IsMatch(className.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                //if (validate == false)
                //{
                //    var obj = new
                //    {
                //        devMsg = Properties.Resources.MISABadrequest_400_Email,
                //        userMsg = Properties.Resources.MISABadrequest_400_Email,
                //        errorCode = "Misa001",
                //        moreInfor = "google.com"
                //    };
                //    return StatusCode(400, obj);
                //}

                ////Với CustomerCode
                ////Kiểm tra rỗng
                //if (customer.CustomerCode == "")
                //{
                //    var obj = new
                //    {
                //        devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
                //        userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
                //        errorCode = "Misa001",
                //        moreInfor = "google.com"
                //    };
                //    return StatusCode(400, obj);
                //}

                ////Kiểm tra trùng
                //var sqlComman1 = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                //var check = dbConnection.QueryFirstOrDefault(sqlComman1, param: parameters);
                //if (check != null)
                //{
                //    var obj = new
                //    {
                //        devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                //        userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                //        errorCode = "Misa001",
                //        moreInfor = "google.com"
                //    };
                //    return BadRequest(400, obj);
                //}

                // columsName = columsName.Remove(columsName.Length - 1, 1);
                parameters.Add($"@{className}Id", entityId);
                columsParam = columsParam.Remove(columsParam.Length - 1, 1);

                var sqlComman = $"UPDATE {className} SET {columsParam} WHERE {className}Id=@{className}Id";
                var rowEffect = dbConnection.Execute(sqlComman, param: parameters);
                return rowEffect;
            }
        }
        
        /// <summary>
        /// Xóa thực thể theo Id
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Số dòng bị thay đổi: != 0 - thành công, = 0 Thất bại</returns>
        public int Delete(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 

            //2. Khởi tạo đối tượng kết nối với db
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo dynamicParam: 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}", entityId);

                var sqlComman = $"DELETE FROM {className} WHERE {className}Id = @{className}";

                var rowEffect = dbConnection.Execute(sqlComman, parameters);

                return rowEffect;
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã thực thể
        /// </summary>
        /// <typeparam name="MISAEntity">Kiểu thực thể</typeparam>
        /// <param name="entityCode">mã thực thể</param>
        /// <returns>kết quả kiểm tra: true - Không có mã trùng, false có mã trùng</returns>
        public bool CheckCodeDuplicate(string entityCode)
        {
            // Nên check theo CustomAtrribute
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}Code", entityCode);

            //2. Khởi tạo đối tượng kết nối với db
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlComman = $"SELECT {className}Code FROM {className} WHERE {className}Code = @{className}Code";
                var isExsist = dbConnection.QueryFirstOrDefault<string>(sqlComman, parameters);

                if(isExsist != null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
