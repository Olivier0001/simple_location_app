using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.DataAccess.Repository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;
using Microsoft.AspNetCore.Authorization;

namespace SimpleLocationWeb.Pages.Admin.CarBrands
{
    [Authorize(Roles = "Manager")]
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CarBrand CarBrand { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            CarBrand = _unitOfWork.CarBrand.GetFirstOrDefault(u => u.Id == id);
            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u => u.Id == id);
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CarBrand.Update(CarBrand);
                _unitOfWork.Save();
                TempData["success"] = "CarBrand update successfully";
                return RedirectToPage("/Admin/CarBrands/Index");
            }
            return Page();
        }
    }
}
