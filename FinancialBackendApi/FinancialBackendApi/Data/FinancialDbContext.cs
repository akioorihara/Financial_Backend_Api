using FinancialBackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Data
{
    public class FinancialDbContext : DbContext
    {
        // DbSets are how EF Core knows which entities to include to the model and how to map them to database tables.
        public DbSet<FileHeader> fileHeaders { get; set; }
        public DbSet<FileDetail> fileDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection string here if not using dependency injection.
            // For example, you can use a local SQL Server database:
            optionsBuilder.UseSqlServer("ConnectionStrings:DefaultConnection");


        }

    }

}
