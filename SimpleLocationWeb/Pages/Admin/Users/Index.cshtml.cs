using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleLocation.Utility;

namespace SimpleLocationWeb.Pages.Admin.Users
{
    [Authorize(Roles = "Manager,Employee")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
