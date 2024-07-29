using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;

namespace PccOnboarding.Context;

public class TSC_Logs_Context() : DbContext
{
    public DbSet<OnboardingSimilarLogsTable> OnboardingSimilarLogsTable { get; set; }
    public DbSet<BedLogsTable> BedLogsTable { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=Logs;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;");
        //base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
