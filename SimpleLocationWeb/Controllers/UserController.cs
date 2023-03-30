using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;

namespace SimpleLocationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userList = _unitOfWork.User.GetAll();
            return Json(new { data = userList });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var objFromDb = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.User.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
