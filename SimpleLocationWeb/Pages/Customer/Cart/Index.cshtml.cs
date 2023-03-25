using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace SimpleLocationWeb.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
        }

        public IEnumerable<LocationCarCart> LocationCarCartList { get; set; }


        public double CartTotal { get; set; }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                LocationCarCartList = _unitOfWork.LocationCarCart.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "Car,Car.CarBrand,Car.Category");

                foreach (var cartItem in LocationCarCartList)
                {
                    CartTotal += (cartItem.Car.Price * cartItem.Count);
                }

                CartTotal = Math.Round((CartTotal), 2);
            }
        }

        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.LocationCarCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.LocationCarCart.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.LocationCarCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count == 1)
            {
                _unitOfWork.LocationCarCart.Remove(cart);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.LocationCarCart.DecrementCount(cart, 1);
            }

            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.LocationCarCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.LocationCarCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
