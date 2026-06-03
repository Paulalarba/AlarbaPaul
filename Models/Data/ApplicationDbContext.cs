using Microsoft.EntityFrameworkCore;
using PaulAlarba.Models;

namespace PaulAlarba.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
    }
}
