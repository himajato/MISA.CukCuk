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
    public class CustomerService : BaseService, ICustomerService
    {
        //ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;
        public CustomerService(IBaseRepository baseRepository):base(baseRepository)
        {
            _serviceResult = new ServiceResult();
        }

        public ServiceResult Add(Customer customer)
        {
            return base.Add<Customer>(customer);
        }

        public ServiceResult Update(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }

        //public ServiceResult Add<Customer>(Customer customer)
        //{
        //    // Xử lí nghiệp vụ
        //    //3.1 Validate dữ liệu
        //    //Với email
        //    var validate = Regex.IsMatch(customer.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        //    if (validate == false)
        //    {
        //        var obj = new
        //        {
        //            devMsg = Properties.Resources.MISABadrequest_400_Email,
        //            userMsg = Properties.Resources.MISABadrequest_400_Email,
        //            errorCode = "Misa001",
        //            moreInfor = "google.com"
        //        };
        //        _serviceResult.IsValid = false;
        //        _serviceResult.Data = obj;
        //        return _serviceResult;
        //    }

        //    //Với CustomerCode
        //    //Kiểm tra rỗng
        //    if (customer.CustomerCode == "")
        //    {
        //        var obj = new
        //        {
        //            devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
        //            userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Empty,
        //            errorCode = "Misa001",
        //            moreInfor = "google.com"
        //        };
        //        _serviceResult.IsValid = false;
        //        _serviceResult.Data = obj;
        //        return _serviceResult;
        //    }

        //    //Kiểm tra trùng
        //    var sqlComman1 = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
        //    var check = dbConnection.QueryFirstOrDefault(sqlComman1, param: parameters);
        //    if (check != null)
        //    {
        //        var obj = new
        //        {
        //            devMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
        //            userMsg = Properties.Resources.MISABadrequest_400_CustomerCode_Duplicate,
        //            errorCode = "Misa001",
        //            moreInfor = "google.com"
        //        };
        //        _serviceResult.IsValid = false;
        //        _serviceResult.Data = obj;
        //        return _serviceResult;
        //    }


        //    // Tương tác với liên kết DataBase
        //    _serviceResult.Data = _customerRepository.AddCustomer(customer);

        //    return _serviceResult;
        //}

        //public ServiceResult Update(Customer customer, Guid customerId)
        //{
        //    // Xử lí nghiệp vụ

        //    // Tương tác với liên kết DataBase
        //    _serviceResult.Data = _customerRepository.UpdateCustomer(customer, customerId);

        //    return _serviceResult;
        //}
    }
}
