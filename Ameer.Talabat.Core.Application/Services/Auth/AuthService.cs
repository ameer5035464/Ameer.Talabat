using Ameer.Talabat.Core.Application.Abstraction.Models.Auth;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.ExceptionsHandlers;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ameer.Talabat.Core.Application.Services.Auth
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtSettings _jwtSettings;

		public AuthService(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IOptions<JwtSettings> jwtSettings)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtSettings = jwtSettings.Value;
		}

		public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
		{
			var user = await _userManager.FindByEmailAsync(loginDTO.Email) ?? throw new UnauthorizedException("Login Invalid");

			var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				var userToken = await CreateJwtToken(user);

				var response = new UserDTO
				{
					Id = user.Id,
					Email = loginDTO.Email,
					Token = new JwtSecurityTokenHandler().WriteToken(userToken)
				};
				return response;
			}

			if (result.IsLockedOut)
			{
				throw new UnauthorizedException("Your Account has locked!");
			}
			if (result.IsNotAllowed)
			{
				throw new UnauthorizedException("Your Account didnt verified yet!");

			}
			if (result.RequiresTwoFactor)
			{
				throw new UnauthorizedException("require two factor  authentication!");
			}
			else
			{
				throw new UnauthorizedException("Login invalid");
			}

		}

		public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
		{
			var checkIfExist = await _userManager.FindByEmailAsync(registerDTO.Email);
			
			if (checkIfExist != null)
			{
				throw new ConflictException("Email already Exist!");
			}

			var user = new ApplicationUser
			{
				UserName = registerDTO.Email,
				Email = registerDTO.Email,
				FirstName = registerDTO.FirstName,
				LastName = registerDTO.LastName,
				PhoneNumber = registerDTO.PhoneNumber,
			};

			var response = await _userManager.CreateAsync(user, registerDTO.Password);
          

            if (response.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "User");

				var jwtToken = await CreateJwtToken(user);

				var userDto = new UserDTO
				{
					Email = user.Email,
					Id = user.Id,
					Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
				};

                return userDto;
			}

			else
			{
				var errors = response.Errors.Select(err => err.Description).ToArray();

				throw new CustomExceptionError("errors on registering user")
				{
					Errors = errors
				};
			}
		}

		public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var rolesAsClaims = new List<Claim>();

			foreach (var role in roles)
			{
				rolesAsClaims.Add(new Claim(ClaimTypes.Role, role));
			}

			var claims = new[]
			{
				new Claim(ClaimTypes.PrimarySid, user.Id),
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.GivenName,user.FirstName),
			}
			.Union(userClaims)
			.Union(rolesAsClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSh	a256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddHours(_jwtSettings.Expires),
				signingCredentials: signingCredentials
				);

			return jwtSecurityToken;
		}

	}
}
