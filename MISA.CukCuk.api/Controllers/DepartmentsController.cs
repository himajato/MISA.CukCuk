using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseEntityController<Department>
    {
    }
}
