using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proj1.Models;
namespace proj1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Providers> Providers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
