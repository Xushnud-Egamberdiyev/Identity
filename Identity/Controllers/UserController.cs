using Identity.DTOs;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UserController(UserManager<ApUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = new ApUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Age = registerDto.Age,
                Status = registerDto.Status,
                CreateData = DateTimeOffset.UtcNow,
            };

            var result = await _userManager.CreateAsync(model, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            foreach(var role in registerDto.Roles)
            {
                await _userManager.AddToRoleAsync(model,role);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> GetAllUsers()
        {
            var result = await _userManager.Users.ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDTO>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return Unauthorized("User Not Found");
            }

            var test = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!test)
            {
                return Unauthorized("Password invalide");
            }

            var token = await _authService.GenerateToken(user);

            return Ok(token);
        } 




    }
}
