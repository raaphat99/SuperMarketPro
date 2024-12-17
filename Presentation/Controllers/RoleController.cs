using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Add");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // Create role
            IdentityRole role = new IdentityRole
            {
                Name = roleVM.Name
            };
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
            }
            return View(roleVM);
        }
    }
}
