﻿using Microsoft.EntityFrameworkCore;
using SimpleLocationWeb.NewFolder;

namespace SimpleLocationWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
    }
}
