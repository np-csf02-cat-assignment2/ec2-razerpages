using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainService.Data
{
    public class MainServiceContext : DbContext
    {
        public MainServiceContext(DbContextOptions<MainServiceContext> options) : base(options)
        {
        }

        public DbSet<MainService.Models.Customer> Customer { get; set; }
    }
}
