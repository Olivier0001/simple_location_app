using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;

namespace SimpleLocationWeb.Pages.Customer.Home
{
    [BindProperties]
    public class CarDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Car Car { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CarBrandList { get; set; }

        public CarDetailsModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            Car = new();
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id);

            }
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            CarBrandList = _unitOfWork.CarBrand.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }


    }
}
