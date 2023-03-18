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
    public class CarTypeRepository : Repository<CarType>, ICarTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CarTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CarType obj)
        {
            var objFromDb = _db.CarType.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;
        }
    }
}
