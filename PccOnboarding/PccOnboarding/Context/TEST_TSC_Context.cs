using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;

namespace PccOnboarding;

public class TEST_TSC_Context(DbContextOptions<TEST_TSC_Context> options) : DbContext(options)
{
    public DbSet<ClientInfoTable> ClientInfoTable { get; set; }
    public DbSet<PccPatientsClientTable> PccPatientsClientTable { get; set; }
    public DbSet<FacilitiesTable> FacilitiesTable { get; set; }
    public DbSet<PccFacilitiesTable> PccFacilitiesTable { get; set; }
    public DbSet<PccAdtTable> PccAdtTable { get; set; }
    public DbSet<ClientActiveTable> ClientActiveTable { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
