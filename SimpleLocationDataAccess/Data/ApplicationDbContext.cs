﻿using Microsoft.EntityFrameworkCore;
using SimpleLocation.Models;

namespace SimpleLocationWeb.DateAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<CarBrand> CarBrand { get; set; }
        public DbSet<Car> Car { get; set; }
    }
}
