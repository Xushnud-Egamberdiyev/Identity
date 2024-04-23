using Identity.DTOs;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApUser> _userManager;
       

        public AuthService(UserManager<ApUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;

        }




        public async Task<AuthDTO> GenerateToken(ApUser user)
        {

            if(user is null)
            {
                return new AuthDTO()
                {
                    Message = "User is Null",
                    StatusCode = 404
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:secretKey"]!);

            var roles =await _userManager.GetRolesAsync(user);

            List<Claim> claims =
                [
                    new(JwtRegisteredClaimNames.Email, user.Email),
                    new(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new(JwtRegisteredClaimNames.Name, user.FullName),
                    new(JwtRegisteredClaimNames.NameId, user.Id),
                    new(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:ValidAudience"]),
                    new(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:ValidIssuer"]),
                    new(JwtRegisteredClaimNames.Exp, _configuration["JWTSettings:ExpireDate"])
             //       new(ClaimTypes.Role, "Admin")

                ];
           

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDeskriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWTSettings:ExpireDate"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDeskriptor);

            return new AuthDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Message = "Succsessfully created token",
                IsSuccess = true

            };
        }
    }
}
