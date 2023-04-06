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

    public class LocationCarCartRepository : Repository<LocationCarCart>, ILocationCarCartRepository
    {
        private readonly ApplicationDbContext _db;

        public LocationCarCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(LocationCarCart locationCarCart, int count)
        {
            locationCarCart.Count -= count;
            _db.SaveChanges();
            return locationCarCart.Count;
        }

        public int IncrementCount(LocationCarCart locationCarCart, int count)
        {
            locationCarCart.Count += count;
            _db.SaveChanges();
            return locationCarCart.Count;
        }
    }
}
