using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLocation.DataAccess.Repository;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocation.Utility;
using SimpleLocationWeb.DateAccess.Data;

namespace SimpleLocationWeb.Pages.Admin.Users
{
    [Authorize(Roles = "Manager")]
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public User User { get; set; }


        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            User = new();
        }

        public void OnGet(string? id)
        {
            if (id != null)
            {
                //Edit
                User = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);

            }
        }



        public async Task<IActionResult> OnPost()
        {
            

            if (User.Id != null)
            {
                //Delete
                var objFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Id == User.Id);
                _unitOfWork.User.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "User delete successfully";
            }

            return RedirectToPage("./Index");
        }
    }
}
