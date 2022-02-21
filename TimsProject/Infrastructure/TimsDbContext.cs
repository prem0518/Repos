using Microsoft.EntityFrameworkCore;
using TimsProject.Models;
namespace TimsProject.Infrastructure
{
    public class TimsDbContext: DbContext
    {
        public TimsDbContext(DbContextOptions<TimsDbContext> options):base (options)
        {

        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        public DbSet<Employee> Employee { get; set; }
               
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tickets> Tickets { get; set; } 
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
       
    }
}