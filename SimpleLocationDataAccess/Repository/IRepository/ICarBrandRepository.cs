﻿using SimpleLocation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLocation.DataAccess.Repository.IRepository
{
    public interface ICarBrandRepository : IRepository<CarBrand>
    {
        void Update(CarBrand obj);
    }
}
