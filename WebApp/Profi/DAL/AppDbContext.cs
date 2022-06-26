using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Profi.Areas.AdminPanel.Models;
using Profi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profi.DAL
{
    public class AppDbContext : IdentityDbContext<AppAdmin>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Testimonials> Testimonials { get; set; }
    }
}
