using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Viewmodels;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationContext _context;
        public CategoryController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();

            foreach (Category category in categories)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
                categoryViewModels.Add(categoryViewModel);
            }
            return View(categoryViewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ViewAction = "Add";
            ViewBag.SubmitAction = "Create";
            return View(new CategoryViewModel());
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", category);
            }

            Category newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };

            _context.Add(newCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ViewAction = "Edit";
            ViewBag.SubmitAction = "Update";

            Category category = _context.Categories.Find(id);
            if (category != null)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
                return View(categoryViewModel);
            }

            ViewBag.ErrorMessage = $"No category with id: {id} is found!";
            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", category);
            }

            Category categoryToUpdate = _context.Categories.Find(id);
            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
