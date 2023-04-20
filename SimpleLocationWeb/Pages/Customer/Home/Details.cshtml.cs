using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Utility;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SimpleLocationWeb.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public LocationCarCart LocationCarCart { get; set; }

        public IEnumerable<LocationCarCart> LocationCarCartList { get; set; }


        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            LocationCarCart = new()
            {
                UserId = claim.Value,
                Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CarBrand"),
                CarId = id,
            };

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                LocationCarCart locationCarCartFromDb = _unitOfWork.LocationCarCart.GetFirstOrDefault(
                    filter: u => u.UserId == LocationCarCart.UserId && u.CarId == LocationCarCart.CarId);
                if (locationCarCartFromDb == null)
                {
                    _unitOfWork.LocationCarCart.Add(LocationCarCart);
                    _unitOfWork.Save();
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.LocationCarCart.GetAll(u => u.UserId == LocationCarCart.UserId).ToList().Count);
                }
                else
                {
                    _unitOfWork.LocationCarCart.IncrementCount(locationCarCartFromDb, LocationCarCart.Count);
                }
                return RedirectToPage("/Customer/Cart/Summary");
            }
            return Page();
        }
    }
}
