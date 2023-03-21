using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;

namespace SimpleLocationWeb.Pages.Admin.CarBrands
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CarBrand CarBrand { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CarBrand.Add(CarBrand);
                _unitOfWork.Save();
                TempData["success"] = "CarBrand created successfully";
                return RedirectToPage("/Admin/CarBrands/Index");
            }
            return Page();
        }
    }
}
