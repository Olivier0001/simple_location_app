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
    }
}
