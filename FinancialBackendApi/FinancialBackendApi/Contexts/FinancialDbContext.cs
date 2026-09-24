using FinancialBackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Contexts
{
    public class FinancialDbContext : DbContext
    {
        public DbSet<FileHeader> fileHeaders { get; set; }
        public DbSet<FileDetail> fileDetails { get; set; }
    }
}
