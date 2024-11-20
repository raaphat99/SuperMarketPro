using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationContext _context;
        public ProductController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ICollection<Product> products = _context.Products.Include(prd => prd.Category).ToList();

            if (products.Count == 0)
            {
                ViewBag.NoProductMessage = "Product List is Empty. Consider adding new ones!";
                return View(new List<ProductViewModel>());
            }

            List<ProductViewModel> viewProducts = new List<ProductViewModel>();
            foreach (var product in products)
            {
                ProductViewModel viewProduct = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    ImageData = product.ImageData,
                    ImageContentType = product.ImageContentType
                };
                viewProducts.Add(viewProduct);
            }
            return View(viewProducts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Category> categories = _context.Categories.ToList();
            ViewBag.ActionName = "Add";
            ViewBag.Categories = categories;
            return View(new ProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                // If model state is not valid, refill the categories
                List<Category> categories = _context.Categories.ToList();
                ViewBag.Categories = categories;
                return View("Add", product);
            }

            if (product == null)
                return BadRequest();

            Product newProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId
            };

            if (product.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await product.ImageFile.CopyToAsync(memoryStream);
                    newProduct.ImageData = memoryStream.ToArray();
                    newProduct.ImageContentType = product.ImageFile.ContentType;
                }
            }

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Include(prd => prd.Category).FirstOrDefault(prd => prd.Id == id);

            if (product == null)
            {
                ViewBag.ErrorMessage = $"No product with id: {id} is found!";
                return View();
            }

            ProductViewModel productToUpdate = new ProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.Category.Id
            };

            return View(productToUpdate);
        }

        [HttpPost]
        public IActionResult Update(int id, ProductViewModel updatedProduct)
        {
            if (!ModelState.IsValid)
                return View("Edit", updatedProduct);

            Product oldProduct = _context.Products.Include(prd => prd.Category).FirstOrDefault(prd => prd.Id == id);

            if (oldProduct == null)
                return NotFound();

            _context.Products.Update(oldProduct);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteProducts(int categoryId)
        {
            var products = _context.Products.Where(p => p.CategoryId == categoryId).ToList();

            if (products.Count > 0)
            {
                _context.Products.RemoveRange(products);
                _context.SaveChanges();
            }

            return RedirectToAction("Delete", "Category", new { id = categoryId });
        }

    }
}
