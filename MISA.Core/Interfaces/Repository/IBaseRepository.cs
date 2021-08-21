using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {
        List<MISAEntity> GetAll();
        int Add(MISAEntity entity);
        int Update(MISAEntity entity, Guid entityId);
        object GetById(Guid entityId);
        int Delete(Guid entityId);
        bool CheckCodeDuplicate(string entityCode);
    }
}
