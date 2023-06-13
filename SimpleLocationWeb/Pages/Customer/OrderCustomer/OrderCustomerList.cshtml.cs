using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Models.ViewModel;
using System.Globalization;
using System.Security.Claims;

namespace SimpleLocationWeb.Pages.Customer.OrderCustomer
{
    [Authorize(Roles = "Customer")]
    public class OrderCustomerListModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public IEnumerable<OrderHeader> OrderHeaders { get; set; }


        public OrderCustomerListModel(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                OrderHeaders = _unitOfWork.OrderHeader.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "User");
            }
            
        }
    }
}
