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

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User obj)
        {
            var objFromDb = _db.User.FirstOrDefault(u => u.FirstName == obj.FirstName);
            objFromDb.UserName = obj.UserName;
            objFromDb.Email = obj.Email;
            objFromDb.PhoneNumber = obj.PhoneNumber;
            objFromDb.FirstName = obj.FirstName;
            objFromDb.LastName = obj.LastName;
            
        }
    }
}
