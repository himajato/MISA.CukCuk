﻿using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    class EmployeeService : BaseService<Employee>
    {

        /// <summary>
        /// Hàm khởi tạo EmployeeService
        /// </summary> 
        /// created by: NHNGHIA (15/08/2021)
        /// <param name="baseRepository">Kiểu repository mà service muốn sử dụng</param> 
        public EmployeeService(IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
        }          



        /// <summary>
        /// Validate các trường riêng của nhân viên
        /// </summary>
        /// <typeparam name="Employee">Kiểu dữ liệu</typeparam>
        /// <param name="employee">Thực thể</param>
        /// <returns>Kết quả validate: true - các dữ liệu đã được validate đúng định dạng; false - các dữ liệu chưa đúng định dạng</returns>
        /// created by: NHNGHIA (15/08/2021)
        public override bool CustomValidate(Employee employee)
        {
            var isValid = true;

            //Validate email
            var employeeEmail = typeof(Employee).GetProperty("Email").GetValue(employee).ToString();
            var validateEmail = Regex.IsMatch(employeeEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (validateEmail == false)
            {
                isValid = false;

                _serviceReult.Data = new
                {
                    devMsg = Properties.Resources.MISABadrequest_400_Email,
                    userMsg = Properties.Resources.MISABadrequest_400_Email,
                    errCode = Properties.Resources.MISAErroCode,
                    moreInfo = Properties.Resources.MISAErroMoreInfor
                };
            }
            return isValid;
        }
    }
}
