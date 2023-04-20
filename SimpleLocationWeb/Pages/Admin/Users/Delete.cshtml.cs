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
        public OrderHeader OrderHeader { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Car Car { get; set; }


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

                // Recuperer l'ensemble des enregistrements des commandes de l'utilisateur
                var objOrderHeaderFromDb = _unitOfWork.OrderHeader.GetAll(i => i.UserId == User.Id);

                // Recuperer l'ensemble des enregistrements des details des commandes de l'utilisateur
                var objOrderDetailsFromDb = _unitOfWork.OrderDetails.GetAll(i => i.OrderHeader.UserId == User.Id);

                // Pour chaque enregistrement modifier et sauvegarder le statut de la voiture
                foreach (var item in objOrderDetailsFromDb)
                {
                    // Recuperer l'enregistrement de la voiture du details de la commande
                    int carId = item.CarId;
                    var objCarFromDb = _unitOfWork.Car.GetFirstOrDefault(i => i.Id == carId);
                    // Extraire le statut de la voiture
                    var carStatus = objCarFromDb.CarStatus;
                    // Modifier le statut de la voiture et sauvegarder la modification
                    carStatus = "Disponible";
                    objCarFromDb.CarStatus = carStatus;
                    _unitOfWork.Car.Update(objCarFromDb);
                    _unitOfWork.Save();
                }
                // Supprimer l'ensemble des enregistrements des commandes et sauvegarder
                _unitOfWork.OrderHeader.RemoveRange(objOrderHeaderFromDb);
                _unitOfWork.Save();
                // Recuperer l'enregistrement de l'utilisateur
                var objFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Id == User.Id);
                // Supprimer l'enregistrement de l'utilisateur et sauvegarder
                _unitOfWork.User.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Suppression du client réussie";
            }

            return RedirectToPage("./Index");
        }
    }
}
