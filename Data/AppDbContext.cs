using Microsoft.EntityFrameworkCore;
using DoAnWebBanDoChoi.Models;

namespace DoAnWebBanDoChoi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}