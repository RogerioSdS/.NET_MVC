using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Data
{
    public class SalesAppContext : DbContext
    {
        public SalesAppContext (DbContextOptions<SalesAppContext> options)
            : base(options)
        {
        }

        public DbSet<SalesApp.Models.Department> Department { get; set; } = default!;
    }
}
