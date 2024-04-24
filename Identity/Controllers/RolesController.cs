using Identity.DTOs;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<ApUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<ApUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateRole(RoleDto role)
        {

            var result = await _roleManager.FindByNameAsync(role.RoleName);
            
            if (result == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
                return Ok(new ResponseDto
                {
                    Message = "Role created",
                    IsSuccess = true,
                    Code = 201
                });

            }

            return Ok(new ResponseDto
            {
                Message = "Role cann not created",
                Code = 403,

            });
        }
        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return Ok(roles);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRoles(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
                throw new Exception("Role Not Found!");

            var result = await _roleManager.DeleteAsync(role);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(string rolename, string updateRole)
        {
            var role = await _roleManager.FindByNameAsync(rolename);

            if (role is null)
                throw new Exception("Role Not Found!");

            role.Name = updateRole;
            role.NormalizedName = updateRole.ToUpper();

            var update = await _roleManager.UpdateAsync(role);
            
            if(!update.Succeeded)
            {
                throw new Exception("role update failed");
            }

            return Ok(update);
        }
    }
}
