using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostitionsController : BaseEntityController<Position>
    {

        public PostitionsController(IBaseService customerService,IBaseRepository<Position> repository): base(customerService,repository)
        {

        }
    }
}
