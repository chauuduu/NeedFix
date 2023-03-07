using Azure;
using Domain.Staffs;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Service.StaffServices
{
    public class StaffService : IStaffService
    {
        readonly IConfiguration configuration;

        readonly IStaffRepository StaffRepository;

        private readonly UserManager<Staff> userManager;
        private readonly SignInManager<Staff> signInManager;

        public StaffService(IConfiguration configuration, IStaffRepository staffRepository, UserManager<Staff> userManager, SignInManager<Staff> signInManager)
        {
            this.configuration = configuration;
            StaffRepository = staffRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void Login(string email, string password)
        {
            var user = userManager.FindByEmailAsync(email);
            if (user == null) 
            {
                throw new Exception($"Email doesn't exist"); 
            }
            else
            {

                var response = signInManager.PasswordSignInAsync(email, password, true, true);
                if (!response.IsCompletedSuccessfully)
                {
                    throw new Exception($"Password is Incorrect!");
                }
            }    
        }

        public void Logout()
        {
            signInManager.SignOutAsync();
        }

        public void Register(string citizenCode, string fullName, DateTime birthday, string phone, string address, int roleId, string email, string password)
        {
            Staff staff = new Staff(citizenCode,fullName,birthday,phone,address,roleId,email,password);
            var user = userManager.FindByEmailAsync(email);
            if (user != null)
            {
                throw new Exception($"Email is exist");
            }
            var response = userManager.CreateAsync(staff, password);
            if (!response.IsCompletedSuccessfully)
            {
                throw new Exception($"Password is Incorrect!");
            } 
        }

        public string Token(string email, string password)
        {
            var Staff = StaffRepository.GetStaff(email, password);
            if (Staff == null)
            {
                throw new Exception("Invalid credentials");
            }

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", Staff.Id.ToString()),
                        new Claim("FullName",Staff.FullName),
                        new Claim("RoleId",Staff.RoleId.ToString()),
                        new Claim(ClaimTypes.Role, Staff.RoleId==1?"Admin":Staff.RoleId==2?"Accountant":"Other"),
                        new Claim("Email", Staff.Email)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
