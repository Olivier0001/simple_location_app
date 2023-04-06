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
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CarBrand CarBrand { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
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
            var carBrandFromDb = _unitOfWork.CarBrand.GetFirstOrDefault(u => u.Id == CarBrand.Id);
            if (carBrandFromDb != null)
            {
                _unitOfWork.CarBrand.Remove(carBrandFromDb);
                _unitOfWork.Save();
                TempData["success"] = "CarBrand delete successfully";
                return RedirectToPage("/Admin/CarBrands/Index");
            }
            return Page();
        }
    }
}
