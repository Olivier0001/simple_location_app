using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLocation.DataAccess.Repository;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;

namespace SimpleLocationWeb.Pages.Admin.Users
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public User User { get; set; }
      

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //User = new();
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

            //Edit
            var objFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Id == User.Id);
            _unitOfWork.User.Update(User);
            _unitOfWork.Save();
            TempData["success"] = "User edit successfully";
            return RedirectToPage("./Index");
        }
    }
}
