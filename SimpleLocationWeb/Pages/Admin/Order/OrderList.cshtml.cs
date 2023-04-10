using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.DataAccess.Migrations;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models.ViewModel;

namespace SimpleLocationWeb.Pages.Admin.Order
{
    [Authorize]
    public class OrderListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
