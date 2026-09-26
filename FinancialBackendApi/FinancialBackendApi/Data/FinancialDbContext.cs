using FinancialBackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Data
{
    public class FinancialDbContext : DbContext
    {
        // DbSets are how EF Core knows which entities to include to the model and how to map them to database tables.
        public DbSet<FileHeader> fileHeaders { get; set; }
        public DbSet<FileDetail> fileDetails { get; set; }

        //

    }

}
