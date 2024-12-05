using Microsoft.EntityFrameworkCore;
using asbEvent.Models;

namespace asbEvent.Data
{
    public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
                EventRegistrations = Set<EventRegistration>();
                Employees = Set<Employee>();
            }

            public DbSet<EventRegistration> EventRegistrations { get; set; }
            public DbSet<Employee> Employees { get; set; }
        }
}