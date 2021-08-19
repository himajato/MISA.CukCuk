using Dapper;
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
    public class BaseRepository : IBaseRepository
    {
        public int Add<MISAEntity>(MISAEntity entity)
        {
            //// Khởi tạo ID mới cho nhân viên
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
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

        public List<MISAEntity> GetAll<MISAEntity>()
        {
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            using(IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                //3. Lấy dữ liệu:
                var sqlCommand = $"SELECT * FROM {className}";
                var entity = dbConnection.Query<MISAEntity>(sqlCommand);

                return (List<MISAEntity>)entity;
            }
        }

        public object GetById<MISAEntity>(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db CustomerId = @CustomerIdParam
            using(IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                //3. Lấy dữ liệu:
                var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @{className}IdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}IdParam", entityId);

                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);

                return entity;
            }  
        }

        public int Update<MISAEntity>(MISAEntity entity, Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            //IDbConnection dbConnection = new MySqlConnection(connectionString);
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // Khai báo dynamicParam: 
                DynamicParameters parameters = new DynamicParameters();


                //3. Lấy prop name và prop type: 
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

        public int Delete<MISAEntity>(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            //2. Khởi tạo đối tượng kết nối với db
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                // Khai báo dynamicParam: 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}", entityId);

                var sqlComman = $"DELETE FROM {className} WHERE {className}Id = @{className}";

                var rowEffect = dbConnection.Execute(sqlComman, parameters);

                return rowEffect;
            }
        }

        public bool CheckCodeDuplicate<MISAEntity>(string entityCode)
        {
            var className = typeof(MISAEntity).Name;

            //Truy cập vào database MF947_NHNGHIA_CukCuk
            //1.Khai báo thông tin kết nối database: 
            var connectionString = "Host = 47.241.69.179;"
                                   + "Database = MF947_NHNGHIA_CukCuk;"
                                   + "User Id = dev;"
                                   + "Password = 12345678";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}Code", entityCode);

            //2. Khởi tạo đối tượng kết nối với db
            using (IDbConnection dbConnection = new MySqlConnection(connectionString))
            {
                var sqlComman = $"GET {className}Code FROM {className} WHERE {className}Code = @{className}Code";
                var isExsist = dbConnection.Execute(sqlComman, parameters);

                if(isExsist != 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
