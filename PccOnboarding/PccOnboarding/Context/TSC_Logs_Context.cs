using Microsoft.EntityFrameworkCore;

namespace PccOnboarding;

public class TSC_Logs_Context(DbContextOptions<TSC_Logs_Context> options) : DbContext(options)
{
    public DbSet<BedLogsTable> BedLogsTable { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
