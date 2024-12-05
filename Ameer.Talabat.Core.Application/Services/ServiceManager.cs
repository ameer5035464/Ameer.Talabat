using Ameer.Talabat.Core.Application.Abstraction.Models.Auth;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.Services.Auth;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Ameer.Talabat.Core.Application.Services
{
	internal class ServiceManager : IServiceManager
	{
		private readonly IMapper _mapper;
		private readonly IBasketRepository _basketRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IOptions<JwtSettings> _jwtSettings;
		private readonly IUnitOfWork _unitOfWork;
		private readonly Lazy<IProductServices> _productServices;
		private readonly Lazy<IBasketService> _basketServices;
		private readonly Lazy<IAuthService> _authService;
		public ServiceManager(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			IBasketRepository basketRepository,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IOptions<JwtSettings> jwtSettings)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_basketRepository = basketRepository;
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtSettings = jwtSettings;
			_productServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
			_basketServices = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
			_authService = new Lazy<IAuthService>(new AuthService(_userManager, _signInManager, _jwtSettings));
		}
		public IProductServices ProductServices => _productServices.Value;
		public IAuthService AuthServices => _authService.Value;
        public IBasketService BasketServices => _basketServices.Value;
	}
}
