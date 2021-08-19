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
    public class BaseService : IBaseService
    {
        IBaseRepository _baseRepository;
        ServiceResult _serviceReult;

        public BaseService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceReult = new ServiceResult();
        }

        public ServiceResult Add<MISAEntity>(MISAEntity entity)
        {
            //Validate dữ liệu
            
            //Thực hiện thêm mới
           _serviceReult.Data = _baseRepository.Add<MISAEntity>(entity);
           return _serviceReult;
        }

        public ServiceResult Update<MISAEntity>(MISAEntity entity, Guid entityId)
        {
            //Validate dữ liệu

            //Thực hiện thêm mới
            _serviceReult.Data = _baseRepository.Update<MISAEntity>(entity, entityId);
            return _serviceReult;
        }

        public ServiceResult GetAll<MISAEntity>()
        {
            _serviceReult.Data = _baseRepository.GetAll<MISAEntity>();
            return _serviceReult;
        }

        public ServiceResult GetById<MISAEntity>(Guid entityId)
        {
            _serviceReult.Data = _baseRepository.GetById<MISAEntity>(entityId);
            return _serviceReult;
        }

        public bool ValidateEntity<MISAEntity>(MISAEntity entity)
        {

        }
    }
}
