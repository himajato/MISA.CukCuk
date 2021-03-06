using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        //ICustomerRepository _customerRepository;


        /// <summary>
        /// Hàm khởi tạo CustomerService
        /// </summary> 
        /// created by: NHNGHIA (15/08/2021)
        /// <param name="baseRepository">Kiểu repository mà service muốn sử dụng</param> 
        public CustomerService(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {

        }


        /// <summary>
        /// Validate các trường riêng của khách hàng
        /// </summary>
        /// <typeparam name="Customer">Kiểu dữ liệu</typeparam>
        /// <param name="customer"></param>
        /// <returns>Kết quả validate: true - các dữ liệu đã được validate đúng định dạng; false - các dữ liệu chưa đúng định dạng</returns>
        /// created by: NHNGHIA (15/08/2021)
        public override bool CustomValidate(Customer customer)
        {
            var isValid = true;

            //Validate email
            var customerEmail = typeof(Customer).GetProperty("Email").GetValue(customer).ToString();
            var validateEmail = Regex.IsMatch(customerEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (validateEmail == false)
            {
                isValid = false;

                _serviceReult.Data = new
                {
                    devMsg = "Email không đúng định dạng",
                    userMsg = "Email không đúng định dạng",
                    errCode = Properties.Resources.MISAErroCode,
                    moreInfo = Properties.Resources.MISAErroMoreInfor
                };
                return isValid;
            }
            return isValid;
        }
    }
}
