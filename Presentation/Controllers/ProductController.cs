using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork,
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            if (products.Count() == 0)
            {
                ViewBag.NoProductMessage = "Product List is Empty. Consider adding new ones!";
                return View(new List<ProductViewModel>());
            }

            List<ProductViewModel> viewProducts = new List<ProductViewModel>();
            foreach (var product in products)
            {
                ProductViewModel viewProduct = _mapper.Map<ProductViewModel>(product);
                viewProduct.ImageData = product.ImageData;
                viewProduct.ImageContentType = product.ImageContentType;
                viewProducts.Add(viewProduct);
            }
            return View(viewProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return PartialView("_ProductDetailsPartial", product);
        }

        [HttpGet]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = _productService.GetProductsByCategory(categoryId);

            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return PartialView("_ProductsTablePartial", products);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.ActionName = "Add";
            ViewBag.Categories = categories;
            return View(new ProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                // If model state is not valid, refill the categories
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = categories;
                return View("Add", productVM);
            }

            if (productVM == null)
                return BadRequest();

            Product newProduct = _mapper.Map<Product>(productVM);

            if (productVM.ImageFile != null)
            {
                await _productService.HandleImageUpload(productVM.ImageFile, newProduct);
            }

            await _productService.CreateAsync(newProduct);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                ViewBag.ErrorMessage = $"No product with id: {id} is found!";
                return View();
            }

            ProductViewModel productToUpdate = _mapper.Map<ProductViewModel>(product);
            return View(productToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductViewModel updatedProduct)
        {
            if (!ModelState.IsValid)
                return View("Edit", updatedProduct);

            Product product = _mapper.Map<Product>(updatedProduct);

            _productService.Update(product);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
