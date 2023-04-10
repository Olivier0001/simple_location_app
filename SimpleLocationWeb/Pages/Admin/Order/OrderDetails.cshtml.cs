using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLocation.DataAccess.Migrations;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Models.ViewModel;
using SimpleLocation.Utility;
using Stripe.Checkout;
using System.Security.Claims;
using Car = SimpleLocation.Models.Car;
using OrderDetails = SimpleLocation.DataAccess.Migrations.OrderDetails;

namespace SimpleLocationWeb.Pages.Admin.Order
{
    [Authorize]
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailVM OrderDetailVM { get; set; }

        public Car Car { get; set; }

        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            OrderDetailVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList(),
            };

        }


        public IActionResult OnPost(int id)
        {

            OrderDetailVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList(),
            };



            // Recuperer l'enregistrement de la voiture
            var objOrderDetails = _unitOfWork.OrderDetails.GetFirstOrDefault(u => u.OrderId == id);
            int carId = objOrderDetails.CarId;
            if (carId != null)
            {
                Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == carId);
                var car = Car;

                // Recuperer le statut de la voiture et le modifier
                string carStatus = Car.CarStatus;
                string newCarStatus = "Disponible";
                Car.CarStatus = newCarStatus;

                // Mettre a jour la modification et enregistrez la
                _unitOfWork.Car.Update(car);
                _unitOfWork.Save();

                
            }

            //Delete
            var objFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User");
            _unitOfWork.OrderHeader.Remove(objFromDb);

            _unitOfWork.Save();
            TempData["success"] = "Suppression de la location réussie";

            return RedirectToPage("OrderList");
        }

       

    }
}
