using Microsoft.EntityFrameworkCore;

namespace PccOnboarding;

public class TSC_Utilities_Context(DbContextOptions<TSC_Utilities_Context> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
