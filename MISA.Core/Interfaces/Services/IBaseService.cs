using MISA.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService
    {
        /// <summary>
        /// Thêm mới thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>ServiceResult - kết quả xử lý nghiệp vụ</returns>
        /// created by: NHNGHIA (12/08/2021)
        ServiceResult Add<MISAEntity>(MISAEntity entity);

        /// <summary>
        /// Update thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>ServiceResult - kết quả xử lý nghiệp vụ</returns>
        ServiceResult Update<MISAEntiry>(MISAEntiry entity, Guid entityId);
    }
}
