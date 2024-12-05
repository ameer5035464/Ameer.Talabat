using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Dashboard.View_Models.CategoryVMs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.Dashboard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var mapCategory = _mapper.Map<IReadOnlyList<GetCategoriesVm>>(category);

            return View(mapCategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var getCurrentUser = currentUser!.Id;

            var category = _mapper.Map<ProductCategory>(model);
            category.CreatedBy = currentUser.Id;
            category.LastModifiedBy = currentUser.Id;

            try
            {
                await _unitOfWork.GetRepository<ProductCategory, int>().AddAsync(category);
                await _unitOfWork.CompletedAsync();
            }
            catch (Exception)
            {

                return BadRequest("this category already Exist!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.GetRepository<ProductCategory, int>().GetAsync(id);

            try
            {
                if (category is not null)
                {
                    _unitOfWork.GetRepository<ProductCategory, int>().Delete(category!);
                    await _unitOfWork.CompletedAsync();
                }
            }
            catch (Exception)
            {

                return BadRequest("no Category with this Id");
            }
            return RedirectToAction("Index");
        }
    }
}
