using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebNumber4_2
{
    public class AppDbContext : DbContext
    {
        public DbSet<DataEntry> DataEntries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
