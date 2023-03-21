using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocation.Models;
using SimpleLocationWeb.DateAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLocation.DataAccess.Repository
{

    public class CarBrandRepository : Repository<CarBrand>, ICarBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public CarBrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CarBrand obj)
        {
            var objFromDb = _db.CarBrand.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;
        }
    }
}
