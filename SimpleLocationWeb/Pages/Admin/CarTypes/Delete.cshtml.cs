using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.DataAccess.Repository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;

namespace SimpleLocationWeb.Pages.Admin.CarTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CarType CarType { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            CarType = _unitOfWork.CarType.GetFirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u => u.Id == id);
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            var carTypeFromDb = _unitOfWork.CarType.GetFirstOrDefault(u => u.Id == CarType.Id);
            if (carTypeFromDb != null)
            {
                _unitOfWork.CarType.Remove(carTypeFromDb);
                _unitOfWork.Save();
                TempData["success"] = "CarType delete successfully";
                return RedirectToPage("/Admin/CarTypes/Index");
            }
            return Page();
        }
    }
}
