using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using System.Linq;

namespace SimpleLocationWeb.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         
        public IEnumerable<Car> CarList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            CarList = _unitOfWork.Car.GetAll(includeProperties: "Category,CarBrand");
            CategoryList = _unitOfWork.Category.GetAll(orderby: u=>u.OrderBy(c=>c.DisplayOrder));
        }
    }
}
