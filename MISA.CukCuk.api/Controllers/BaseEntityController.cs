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
        IBaseService<MISAEntity> _baseService;
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseEntityController(IBaseService<MISAEntity> baseService, IBaseRepository<MISAEntity> baseRepository)
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
                var entity = _baseRepository.GetAll();
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
                    errorCode = Core.Properties.Resources.MISAErroCode,
                    moreInfor = Core.Properties.Resources.MISAErroMoreInfor,
                };
                return StatusCode(500, obj);
            }
        }

        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var entity = _baseService.GetById(entityId);
                if ((int)entity.Data > 0)
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
                    errorCode = Core.Properties.Resources.MISAErroCode,
                    moreInfor = Core.Properties.Resources.MISAErroMoreInfor
                };
                return StatusCode(500, obj);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var rowEffect = _baseRepository.Delete(entityId);
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
                    errorCode = MISA.Core.Properties.Resources.MISAErroCode,
                    moreInfor = MISA.Core.Properties.Resources.MISAErroMoreInfor,
                };
                return StatusCode(500, obj);
            }
        }
        
        [HttpPost]
        public IActionResult Add(MISAEntity entity)
        {
            try
            {
                var rowEffect = _baseService.Add(entity);
                if (rowEffect.IsValid)
                {
                    if ((int)rowEffect.Data > 0)
                    {
                        return StatusCode(201, rowEffect.Data);
                    }
                    else
                    {
                        return StatusCode(204);
                    }
                }
                else
                {
                    return BadRequest(rowEffect.Data);
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = Core.Properties.Resources.MISAErroCode,
                    moreInfor = Core.Properties.Resources.MISAErroMoreInfor
                };
                return StatusCode(500, obj);
            }
        }

        [HttpPut]
        public IActionResult Update(MISAEntity entity,Guid entityId)
        {
            try
            {
                var rowEffect = _baseService.Update(entity,entityId);
                if (rowEffect.IsValid)
                {
                    if ((int)rowEffect.Data > 0)
                    {
                        return StatusCode(200, rowEffect);
                    }
                    else
                    {
                        return StatusCode(204);
                    }
                } else
                {
                    return BadRequest(rowEffect.Data);
                }
            }
            catch (Exception ex)
            {
                var obj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.MISAException_Error,
                    errorCode = Core.Properties.Resources.MISAErroCode,
                    moreInfor = Core.Properties.Resources.MISAErroMoreInfor
                };
                return StatusCode(500, obj);
            }
        }
    }

}
