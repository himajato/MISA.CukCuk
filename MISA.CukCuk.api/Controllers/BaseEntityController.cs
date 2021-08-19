using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        IBaseService _baseService;
        IBaseRepository _baseRepository;
        public BaseEntityController(IBaseService baseService, IBaseRepository baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }
        /// <summary>
        /// Lấy hết dữ liệu các bản ghi
        /// </summary>
        /// 
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var entity = _baseRepository.GetAll<MISAEntity>();
                if (entity != null)
                {
                    return StatusCode(200, entity);
                }
                else
                {
                    return StatusCode(204);
                }

            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }

        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var entity = _baseService.GetById<MISAEntity>(entityId);
                if (entity.IsValid == true)
                {
                    return StatusCode(200, entity.Data);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var rowEffect = _baseRepository.Delete<MISAEntity>(entityId);
                if (rowEffect > 0)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }
        
        [HttpPost]
        public IActionResult Add(MISAEntity entity)
        {
            try
            {
                var rowEffect = _baseService.Add<MISAEntity>(entity);
                if ((int)rowEffect.Data > 0)
                {
                    return StatusCode(201, rowEffect.Data);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }

        [HttpPut]
        public IActionResult Update(MISAEntity entity,Guid entityId)
        {
            try
            {
                var rowEffect = _baseService.Update<MISAEntity>(entity,entityId);
                if ((int)rowEffect.Data > 0)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = "Misa001",
                    moreInfor = "google.com"
                };
                return StatusCode(500, obj);
            }
        }
    }

}
