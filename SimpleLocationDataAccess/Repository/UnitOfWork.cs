using SimpleLocation.DataAccess.Repository.IRepository;
using SimpleLocationWeb.DateAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLocation.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CarBrand = new CarBrandRepository(_db);
            Car = new CarRepository(_db);
            LocationCarCart = new LocationCarCartRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            User = new UserRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICarBrandRepository CarBrand { get; private set; }
        public ICarRepository Car { get; private set; }
        public ILocationCarCartRepository LocationCarCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IUserRepository User { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
