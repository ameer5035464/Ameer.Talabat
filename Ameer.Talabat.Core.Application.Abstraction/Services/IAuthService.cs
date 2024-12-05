using Ameer.Talabat.Core.Application.Abstraction.Models.Auth;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser applicationUser);

    }
}
