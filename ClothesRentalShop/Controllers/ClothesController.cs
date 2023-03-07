using Application.Service.ClothService;
using ClothesRentalShop.Profiles;
using ClothesRentalShop.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothesRentalShop.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ClothesController : ControllerBase
    {
        readonly IClothesService clothesService;
        readonly IMapperObjectToObject mapper;

        public ClothesController(IClothesService clothesService, IMapperObjectToObject mapper)
        {
            this.clothesService = clothesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(string? key, int? pageSize, int? page)
        {
            try
            {
                var rs = clothesService.GetList(key, pageSize, page);
                return Ok(mapper.ConvertListClothes(rs));
           
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
                var rs = clothesService.GetById(id);
                if (rs == null)
                {
                    return NotFound();
                }
                return Ok(mapper.ConvertClothes(rs));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(ClothesViewModel clothes )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clothesService.Add(clothes.Name, clothes.Description, clothes.Size, clothes.Price, clothes.RentalPrice, clothes.TypeClothesId, clothes.OriginId, clothes.Status);
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
        public IActionResult Update(int id, ClothesViewModel clothes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clothesService.Update(id, clothes.Name, clothes.Description, clothes.Size, clothes.Price, clothes.RentalPrice, clothes.TypeClothesId, clothes.OriginId, clothes.Status);
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
                clothesService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

