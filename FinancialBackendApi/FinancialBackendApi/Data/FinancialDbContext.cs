using FinancialBackendApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Data
{
    public class FinancialDbContext : DbContext
    {
        // DbSets are how EF Core knows which entities to include to the model and how to map them to database tables.
        public DbSet<FileHeader> FileHeaders { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialDbContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options for the context.
        /// </param>
        public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
            : base(options) { }

    }

}
