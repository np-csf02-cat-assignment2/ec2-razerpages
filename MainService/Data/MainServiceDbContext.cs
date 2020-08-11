using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainService.Models;

namespace MainService.Data
{
    public class MainServiceDbContext : DbContext
    {
        public MainServiceDbContext(DbContextOptions<MainServiceDbContext> options) : base(options)
        {
        }

        public DbSet<MainService.Models.Customer> Customer { get; set; }

        public DbSet<MainService.Models.ContactUs> ContactUs { get; set; }
    }
}
