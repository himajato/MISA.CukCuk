using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository : IBaseRepository
    {
        public int Add<MISAEntity>(MISAEntity entity)
        {
            return 
        }

        public List<MISAEntity> GetAll<MISAEntity>()
        {
            throw new NotImplementedException();
        }

        public int Update<MISAEntity>(MISAEntity entity, Guid entityId)
        {

            throw new NotImplementedException();
        }
    }
}
