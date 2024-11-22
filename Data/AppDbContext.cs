using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem.Models;
using System.Reflection.Emit;
namespace ContractMonthlyClaimSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ClaimModel> Claims { get; set; }
        public DbSet<cmcs_userTBLModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClaimModel>().ToTable("claimTBL");
            modelBuilder.Entity<cmcs_userTBLModel>().ToTable("cmcs_userTBL");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:jkingserver.database.windows.net,1433;Initial Catalog=test;Persist Security Info=False;User ID=Jking;Password=Fr@ney04;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
