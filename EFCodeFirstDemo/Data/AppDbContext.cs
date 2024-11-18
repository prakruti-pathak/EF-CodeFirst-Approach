using EFCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCodeFirstDemo.Data
{
    public class AppDbContext:DbContext
    {
        /// <summary>
        /// contructor of AppDbContext which hold configurations settings(configured with program.cs)
        ///  </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }


    }
}
