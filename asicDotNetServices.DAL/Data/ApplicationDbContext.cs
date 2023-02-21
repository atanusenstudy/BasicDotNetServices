using BasicDotNetServices.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
