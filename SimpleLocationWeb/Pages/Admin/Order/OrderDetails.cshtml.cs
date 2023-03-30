using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Models.ViewModel;

namespace SimpleLocationWeb.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailVM OrderDetailVM { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public OrderDetails OrderDetails { get; set; }

        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            OrderDetailVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(u=>u.OrderId==id).ToList()
            };
        }

        public async Task<IActionResult> OnPost(int id)
        {

            OrderDetailVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList()
            };

            //Delete
            var objFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User");
            _unitOfWork.OrderHeader.Remove(objFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Order delete successfully";

            return RedirectToPage("./OrderList");
        }

    }
}
