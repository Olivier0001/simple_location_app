using SimpleLocation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLocation.DataAccess.Repository.IRepository
{
    public interface ILocationCarCartRepository : IRepository<LocationCarCart>
    {
        int IncrementCount(LocationCarCart car, int count);
        int DecrementCount(LocationCarCart car, int count);
    }
}
