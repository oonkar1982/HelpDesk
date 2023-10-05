

using HelpDesk.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HelpDesk.UI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Company> companies { get; set; }
        public DbSet<UserDetail> users { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Incident> incidents { get; set; }
        public DbSet<StringMap> map { get; set; }
        public DbSet<Contact> contacts { get; set; }



    }
}
