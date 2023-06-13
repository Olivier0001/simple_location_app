using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Security.Application;
using SimpleLocation.DataAccess.Repository;
using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;

namespace SimpleLocationWeb.Pages.Admin.Cars
{
    [Authorize(Roles = "Manager")]
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Car Car { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CarBrandList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            Car = new();
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id);

            }
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            CarBrandList = _unitOfWork.CarBrand.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {

            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (Car.Id == 0)
            {
                //create
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\cars");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                Car.Image = @"\images\cars\" + fileName_new + extension;
                Car.CarStatus = "Disponible";
                string safeHtmlFragmentCarName = Sanitizer.GetSafeHtmlFragment(Car.Name);
                Car.Name = safeHtmlFragmentCarName;
                string safeHtmlFragmentCarDescription = Sanitizer.GetSafeHtmlFragment(Car.Description);
                Car.Description = safeHtmlFragmentCarDescription;
                _unitOfWork.Car.Add(Car);
                _unitOfWork.Save();
                TempData["success"] = "Création de la voiture réussie";
            }
            else
            {
                //Edit
                var objFromDb = _unitOfWork.Car.GetFirstOrDefault(i => i.Id == Car.Id);

                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\cars");
                    var extension = Path.GetExtension(files[0].FileName);

                    //Delete the old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    //new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    Car.Image = @"\images\cars\" + fileName_new + extension;
                }
                else
                {
                    Car.Image = objFromDb.Image;

                }
                string safeHtmlFragmentCarName = Sanitizer.GetSafeHtmlFragment(Car.Name);
                Car.Name = safeHtmlFragmentCarName;
                string safeHtmlFragmentCarDescription = Sanitizer.GetSafeHtmlFragment(Car.Description);
                Car.Description = safeHtmlFragmentCarDescription;
                _unitOfWork.Car.Update(Car);
                _unitOfWork.Save();
                TempData["success"] = "Modification de la voiture réussie";
            }
            return RedirectToPage("./Index");
        }
    }
}
