﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;

namespace SimpleLocationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var OrderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "User");
            return Json(new { data = OrderHeaderList });
        }


    }
}