using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository
    {
        List<MISAEntity> GetAll<MISAEntity>();
        int Add<MISAEntity>(MISAEntity entity);
        int Update<MISAEntity>(MISAEntity entity, Guid entityId);
        object GetById<MISAEntity>(Guid entityId);
        int Delete<MISAEntity>(Guid entityId);
        bool CheckCodeDuplicate<MISAEntity>(string entityCode);
    }
}
