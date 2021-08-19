using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using Dapper;
using System.Text.RegularExpressions;
using MISA.Core.Interfaces.Services;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;

namespace MISA.CukCuk.api.Controllers
{ 
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : BaseEntityController<Customer>
    {
        //ICustomerService _customerService;
        //ICustomerRepository _customerRepository;
        public CustomersController(IBaseService customerService, IBaseRepository customerRepository) : base(customerService, customerRepository)
        {
            //_customerService = customerService;
            //_customerRepository = customerRepository;
        }
    }
}
