using Application.Service.StaffServices;
using ClothesRentalShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClothesRentalShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        readonly IStaffService StaffService;

        public AuthController(IStaffService staffService)
        {
            StaffService = staffService;
        }
        
        [HttpPost("login")]
        public IActionResult Login(UserInfoViewModel StaffData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StaffService.Login(StaffData.Email, StaffData.Password);
                    return Ok(StaffService.Token(StaffData.Email, StaffData.Password));
                }
                return UnprocessableEntity(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            try
            {
                    StaffService.Logout();
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost("register")]
        public IActionResult Register(StaffViewModel StaffData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StaffService.Register(StaffData.CitizenCode, StaffData.FullName, StaffData.Birthday, StaffData.Phone, StaffData.Address, StaffData.RoleId, StaffData.Email, StaffData.Password);
                    return Ok();
                }
                return UnprocessableEntity(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
