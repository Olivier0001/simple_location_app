using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Models.ViewModel;
using System.Security.Claims;

namespace SimpleLocationWeb.Pages.Customer.OrderCustomer
{
    [Authorize]
    public class OrderCustomerDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailVM OrderDetailVM { get; set; }

        public Car Car { get; set; }

        public OrderCustomerDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                OrderDetailVM = new()
                {
                    OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(filter: u => u.UserId == claim.Value, includeProperties: "User"),
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList(),
                };
            }
        }

        public IActionResult OnPost(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                OrderDetailVM = new()
                {
                    OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList(),
                };



                // Recuperer l'enregistrement de la voiture
                var objOrderDetails = _unitOfWork.OrderDetails.GetFirstOrDefault(u => u.OrderId == id);
                int carId = objOrderDetails.CarId;
                Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == carId);
                var car = Car;

                // Recuperer le statut de la voiture et le modifier
                string carStatus = Car.CarStatus;
                string newCarStatus = "Disponible";
                Car.CarStatus = newCarStatus;

                // Mettre a jour la modification et enregistrez la
                _unitOfWork.Car.Update(car);
                _unitOfWork.Save();


                //Delete
                var objFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User");
                _unitOfWork.OrderHeader.Remove(objFromDb);





                _unitOfWork.Save();
                TempData["success"] = "Suppression de la location réussie";

                return RedirectToPage("OrderCustomerList");
            }

            return Page();
        }
    }
}
