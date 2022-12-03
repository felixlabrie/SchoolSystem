using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
