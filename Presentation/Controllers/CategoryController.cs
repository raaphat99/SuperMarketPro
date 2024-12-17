using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using AutoMapper;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModels;


namespace Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, ICategoryService categoryService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();

            foreach (Category category in categories)
            {
                CategoryViewModel categoryViewModel = _mapper.Map<CategoryViewModel>(category);
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
        public async Task<IActionResult> Create(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", categoryVM);
            }

            Category newCategory = _mapper.Map<Category>(categoryVM);

            await _categoryService.CreateAsync(newCategory);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ViewAction = "Edit";
            ViewBag.SubmitAction = "Update";

            Category category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                CategoryViewModel categoryViewModel = _mapper.Map<CategoryViewModel>(category);
                return View(categoryViewModel);
            }

            ViewBag.ErrorMessage = $"No category with id: {id} is found!";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", categoryVM);
            }

            Category category = _mapper.Map<Category>(categoryVM);
            _categoryService.Update(category);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
