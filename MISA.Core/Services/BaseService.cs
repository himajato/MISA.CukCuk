using MISA.Core.Atribute;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceReult;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseRepository">Kiểu repository của thực thể</param>
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceReult = new ServiceResult();
        }

        /// <summary>
        /// Thêm mới thực thể
        /// </summary>
        /// <param name="entity">thực thể</param>
        /// <returns>Kết quả sử lí nghiệp vụ</returns>
        public ServiceResult Add(MISAEntity entity)
        {
            var className = typeof(MISAEntity).Name;
            var classNameCode = $"{className}Code";
            //Validate dữ liệu chung de bug chay lai cho nay di a
            _serviceReult.IsValid = ValidateEntity(entity);

            //Validate dữ liệu riêng  
            if ((_serviceReult.IsValid == true) && (CustomValidate(entity) == false))
            {

                _serviceReult.IsValid = false;
            }

            //Thực hiện check trùng code
            if(_serviceReult.IsValid)
            {
                var code = typeof(MISAEntity).GetProperty(classNameCode).GetValue(entity);
                _serviceReult.IsValid = _baseRepository.CheckCodeDuplicate((string)code);
                _serviceReult.Data = new
                {
                    devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                    userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
                    errCode = Properties.Resources.MISAErroCode,
                    moreInfo = Properties.Resources.MISAErroMoreInfor
                };
            }

            if (_serviceReult.IsValid == true)
            {
                //Thực hiện thêm mới
                _serviceReult.Data = _baseRepository.Add(entity);
            }
            return _serviceReult;
            
        }

        /// <summary>
        /// Sửa thông tin thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể muốn sửa</param>
        /// <param name="entityId">Id của thực thể muốn sửa</param>
        /// <returns></returns>
        public ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            //Validate dữ liệu
            //(Check có tồn tại trong Db k)
            //Thực hiện thêm mới
            _serviceReult.Data = _baseRepository.Update(entity, entityId);
            return _serviceReult;
        }

        public ServiceResult GetAll()
        {
            _serviceReult.Data = _baseRepository.GetAll();
            return _serviceReult;
        }

        /// <summary>
        /// Lấy thông tin thực thể theo Id
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Kết quả sử lí nghiệp vụ</returns>
        public ServiceResult GetById(Guid entityId)
        {
            _serviceReult.Data = _baseRepository.GetById(entityId);
            return _serviceReult;
        }
        /// <summary>
        /// Validate dữ liệu chung 
        /// </summary>
        /// <typeparam name="MISAEntity">Kiểu của thực thể</typeparam>
        /// <param name="entity">thực thể</param>
        /// <returns>kết quả validate (true - đúng định dạng ; false - sai định dạng)</returns>
        public bool ValidateEntity(MISAEntity entity)
        {
            var isValid = true;
            //Lấy ra các properties của thực thể
            var properties = typeof(MISAEntity).GetProperties();

            //Lấy giá trị của các properties
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);

                var misaRequires = prop.GetCustomAttributes(typeof(MISARequire), true);
                if(misaRequires.Length > 0)
                {
                    var fieldName = ((MISARequire)misaRequires[0])._fieldName;
                    if((prop.PropertyType == typeof(string)) && (propValue.ToString() == string.Empty))
                    {
                        isValid = false;
                        _serviceReult.Data = new
                        {
                            devMsg = $"{Properties.Resources.MISARequireFieldEmpty}" + " : " + $"{fieldName}",
                            userMsg = $"{Properties.Resources.MISARequireFieldEmpty}" + " : " + $"{fieldName}",
                            errorCode = Properties.Resources.MISAErroCode,
                            moreInfo = Properties.Resources.MISAErroMoreInfor
                        };
                        return false;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Hàm validate dữ liệu riêng của mỗi thực thể,sẽ được override
        /// </summary>
        /// <param name="entity">Thực thể muốn validate dữ liệu</param>
        /// <returns>True: Đã được validate; False: Chưa đúng định dạng</returns>
        public virtual bool CustomValidate(MISAEntity entity)
        {
            return true;
        }

        
    }
}
