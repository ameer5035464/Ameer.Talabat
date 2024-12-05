using Ameer.Talabat.Core.Application.Abstraction.Models.ProductDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.Specifications;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Core.Domain.Entities.Products;
using Ameer.Talabat.Dashboard.View_Models.ProductsVMs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.Dashboard.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPhotoService photoService,
            UserManager<ApplicationUser> userManager
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var specs = new ProductSpecifications();
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllSpecAsync(specs);
            var mapProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return View(mapProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            ViewBag.Brands = brands;
            ViewBag.Categories = categories;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEditProductVM model)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            ViewBag.Brands = brands;
            ViewBag.Categories = categories;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var uploadPhoto = await _photoService.AddPhotoAsync(model.Image!);
            if (uploadPhoto != null)
            {
                model.PictureUrl = uploadPhoto.Url.ToString();
            }

            var mapProduct = _mapper.Map<Product>(model);

            var getLoggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            mapProduct.CreatedBy = getLoggedInUser!.Id;
            mapProduct.LastModifiedBy = getLoggedInUser!.Id;

            await _unitOfWork.GetRepository<Product, int>().AddAsync(mapProduct);
            await _unitOfWork.CompletedAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            ViewBag.Brands = brands;
            ViewBag.Categories = categories;

            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);

            if (product != null)
            {
                var mapProduct = _mapper.Map<EditProductVM>(product);
                //ViewBag.createdBy = product.CreatedBy;
                return View(mapProduct);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductVM model)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);


            ViewBag.Brands = brands;
            ViewBag.Categories = categories;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mapProduct = _mapper.Map<Product>(model);


            if (model.Image != null)
            {
                var photo = await _photoService.AddPhotoAsync(model.Image!);
                mapProduct.PictureUrl = photo.Url.ToString();
            }
            else
                mapProduct.PictureUrl = model.PictureUrl;


            mapProduct.LastModifiedBy = currentUser!.Id;

            //var photo = await _photoService.AddPhotoAsync(model.Image!);
            //mapProduct.Id = id;


            _unitOfWork.GetRepository<Product, int>().Update(mapProduct);
            await _unitOfWork.CompletedAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetAsync(id);
            if (product != null)
            {
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompletedAsync();
                return RedirectToAction("Index");
            }
            throw new Exception();

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var spec = new ProductSpecifications();
            spec.Criteria = p => p.Id == id;

            var product = await _unitOfWork.GetRepository<Product, int>().GetSpecAsync(spec);

            if (product != null)
            {
                var mapProduct = _mapper.Map<ProductDetailsVM>(product);
                return View(mapProduct);
            }
                throw new Exception("No Product with this ID");
            
        }
    }
}
