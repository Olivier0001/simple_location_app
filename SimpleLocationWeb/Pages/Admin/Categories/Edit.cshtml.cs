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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Category Category { get; set; }

        public EditModel(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }




        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u => u.Id == id);
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault();
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
                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData["success"] = "Modification de la catégorie réussie";
                return RedirectToPage("/Admin/Categories/Index");
            }
            return Page();
        }
    }
}
