
using Application.Service.OriginsService;
using ClothesRentalShop.Profiles;
using ClothesRentalShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClothesRentalShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OriginController : ControllerBase
    {
        readonly IOriginService OriginService;
        readonly IMapperObjectToObject mapper;
        public OriginController(IOriginService OriginService, IMapperObjectToObject mapper)
        {
            this.OriginService = OriginService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(string? key,int? pageSize, int? page)
        {
            try
            {
                var rs = OriginService.GetList(key, pageSize, page);
                return Ok(mapper.ConvertListOrigin(rs));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                var rs = OriginService.GetById(id);
                if (rs == null)
                {
                    return NotFound();
                }
                return Ok(mapper.ConvertOrigin(rs));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(OriginViewModel origin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OriginService.Add(origin.Name, origin.Address);
                    return Ok();
                }
                return UnprocessableEntity(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, OriginViewModel origin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OriginService.Update(id,origin.Name, origin.Address);
                    return Ok();
                }
                return UnprocessableEntity(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                OriginService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}


