using Ameer.Talabat.APIs.Controllers.Extensions;
using Ameer.Talabat.Core.Application.Abstraction.Models.Auth;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ameer.Talabat.APIs.Controllers.Controllers.Account
{
	public class AccountController : BaseApiController
	{
		private readonly IServiceManager _serviceManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IServiceManager serviceManager,UserManager<ApplicationUser> userManager)
        {
			_serviceManager = serviceManager;
            _userManager = userManager;
        }

		[HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
		{
			var result = await _serviceManager.AuthServices.RegisterAsync(register);

			return Ok(new {result.Token});
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDTO login)
		{
			var result = await _serviceManager.AuthServices.LoginAsync(login);

			return Ok(new { result.Token });
		}

		[Authorize]
		[HttpGet("GetCurrentUser")]
		public async Task<ActionResult<UserDTO>> GetCurrentUser()
		{
			var user = User.FindFirstValue(ClaimTypes.PrimarySid);
			var currentUser = await _userManager.FindByIdAsync(user!);

			var securityToken = await _serviceManager.AuthServices.CreateJwtToken(currentUser!);

			var userDto = new UserDTO
			{
				Id = currentUser!.Id,
				Email = currentUser.Email!,
				Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
			};
			return Ok(userDto);
		}

		[Authorize]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var user = await _userManager.GetCurrentUserWithAddress(User);

			var mapUser = new AddressDto
			{
				City = user.Address!.City,
				Country = user.Address.Country,
				FirstName = user.Address.FirstName,
				LastName = user.Address.LastName,
				Street = user.Address.Street,
			};

			return Ok(mapUser);

        }

		[Authorize]
		[HttpPut("UpdateAddress")]
		public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto addressDto)
		{
			var user = await _userManager.GetCurrentUserWithAddress(User);

            user.Address = new Address 
			{ 
				ApplicationUser = user,
				City = addressDto.City,
				Country = addressDto.Country,
				FirstName = addressDto.FirstName,
				LastName = addressDto.LastName,
				Street= addressDto.Street,
				Id = user.Address!.Id,
				USerId = user.Id
			};

			var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
				return BadRequest();
            }

			return Ok(result);
        }


	}
}
