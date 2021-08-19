using MISA.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService
    {
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>ServiceResult - kết quả xử lý nghiệp vụ</returns>
        /// created by: NHNGHIA (12/08/2021)
        ServiceResult Add(Customer customer);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>ServiceResult - kết quả xử lý nghiệp vụ</returns>
        ServiceResult Update(Customer customer, Guid customerId);

        /// <summary>
        /// Lấy hết dữ liệu của tất cả khách hàng
        /// </summary>
        /// <returns>ServiceResult - kết quả xử lý nghiệp vụ</returns>
        ServiceResult GetAll();


        /// <summary>
        /// Lấy dữ liệu của một khách hàng
        /// </summary>
        /// <returns>ServiceResult - kết quả xử lý nghiệp vụ</returns>
        ServiceResult GetById(Guid CustomerId);
    }
}
