using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Utility;
using Stripe.Checkout;
using System.Security.Claims;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace SimpleLocationWeb.Pages.Customer.Cart
{
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<LocationCarCart> LocationCarCartList { get; set; }
        public IEnumerable<Car> CarList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public OrderDetails OrderDetails { get; set; }

        public Car Car { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
            OrderDetails = new OrderDetails();
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                LocationCarCartList = _unitOfWork.LocationCarCart.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "Car,Car.CarBrand,Car.Category");

                foreach (var cartItem in LocationCarCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.Car.Price * cartItem.Count);
                }

                OrderHeader.OrderTotal = Math.Round((OrderHeader.OrderTotal), 2);

                User user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickupName = user.FirstName;
                OrderHeader.PhoneNumber = user.PhoneNumber;
            }
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                LocationCarCartList = _unitOfWork.LocationCarCart.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "Car,Car.CarBrand,Car.Category");

                foreach (var cartItem in LocationCarCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.Car.Price * cartItem.Count);
                    Car.CarStatus = cartItem.Car.CarStatus;
                    cartItem.Car.CarStatus = "Non Disponible";
                    var test = cartItem.Car;
                    _unitOfWork.Car.Update(test);
                }

               

                OrderHeader.OrderTotal = Math.Round((OrderHeader.OrderTotal), 2);

                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " + OrderHeader.PickUpTime.ToShortTimeString());
                OrderHeader.PickUpDate = OrderHeader.PickUpDate;
                OrderHeader.PickTimeOfReturn = OrderHeader.PickTimeOfReturn;
                OrderHeader.PickDateOfReturn = OrderHeader.PickDateOfReturn;
                OrderHeader.OrderHeaderStatus = "Validé";
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in LocationCarCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        CarId = item.CarId,
                        OrderId = OrderHeader.Id,
                        Name = item.Car.Name,
                        Price = item.Car.Price,
                        Count = item.Count
                    };
                    //string test = OrderDetails.Car.CarStatus;
                    _unitOfWork.OrderDetails.Add(orderDetails);
                }



                int quantity = LocationCarCartList.ToList().Count;
                _unitOfWork.LocationCarCart.RemoveRange(LocationCarCartList);
                _unitOfWork.Save();

                var domain = "https://localhost:44338/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                      PriceData = new SessionLineItemPriceDataOptions
                      {
                          UnitAmount = (long)OrderHeader.OrderTotal*100,
                          Currency="usd",
                          ProductData= new SessionLineItemPriceDataProductDataOptions
                          {
                              Name = "Simple Location Order",
                              Description = "Total Distinct Item - "+quantity
                          },
                      },
                      Quantity = quantity
                  },
                },
                    Mode = "payment",
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "customer/cart/index",
                };
                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

            return Page();
        }

    }
}
