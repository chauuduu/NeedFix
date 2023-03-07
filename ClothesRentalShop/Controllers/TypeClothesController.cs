

using Application.Service.TypeClothService;
using ClothesRentalShop.Profiles;
using ClothesRentalShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClothesRentalShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeClothesController : ControllerBase
    {
        readonly ITypeClothesService typeClothesService;
        readonly IMapperObjectToObject mapper;

        public TypeClothesController(ITypeClothesService typeClothesService, IMapperObjectToObject mapper)
        {
            this.typeClothesService = typeClothesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(string? key,int? pageSize,int? page)
        {
            try
            {
                var rs = typeClothesService.GetList(key, pageSize, page);
                return Ok(mapper.ConvertListTypeClothes(rs));
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
                var rs = typeClothesService.GetById(id);
                if (rs == null)
                {
                    return NotFound();
                }
                return Ok(mapper.ConvertTypeClothes(rs));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(TypeClothesViewModel type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    typeClothesService.Add(type.Name, type.Limit);
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
        public IActionResult Update(int id, TypeClothesViewModel type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    typeClothesService.Update(id,type.Name, type.Limit);
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
                typeClothesService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}


