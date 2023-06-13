using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Security.Application;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;

namespace SimpleLocationWeb.Pages.Admin.Categories
{
    [Authorize(Roles = "Manager")]
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "L'ordre ne peut pas correspondre exactement au nom");
            }
            if (ModelState.IsValid)
            {
                string safeHtmlFragment = Sanitizer.GetSafeHtmlFragment(Category.Name);
                Category.Name = safeHtmlFragment;
                _unitOfWork.Category.Add(Category);
                _unitOfWork.Save();
                TempData["success"] = "Création de la catégorie réussie";
                return RedirectToPage("/Admin/Categories/Index");
            }
            return Page();
        }
    }
}
