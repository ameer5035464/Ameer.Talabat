using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Dashboard.View_Models.BrandsVMs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.Dashboard.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BrandController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var mapBrands = _mapper.Map<IReadOnlyList<GetBrandsVM>>(brands);
            return View(mapBrands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateVm model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var getCurrentUser = currentUser!.Id;

            if (!ModelState.IsValid)
            {
                return View();
            }



            var mapBrand = _mapper.Map<ProductBrand>(model);

            mapBrand.CreatedBy = currentUser.Id;
            mapBrand.LastModifiedBy = currentUser.Id;
            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(mapBrand);
                await _unitOfWork.CompletedAsync();
            }
            catch (Exception)
            {

                return BadRequest();
            }


            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);

            if (brand != null)
            {
                _unitOfWork.GetRepository<ProductBrand, int>().Delete(brand);
                await _unitOfWork.CompletedAsync();

            }
            return  RedirectToAction("Index");
        }
    }
}
