using Identity.DTOs;
using Identity.Models;

namespace Identity.Services
{
    public interface IAuthService
    {
        public Task<AuthDTO> GenerateToken(ApUser user);
    }
}
