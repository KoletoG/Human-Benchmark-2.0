using Human_Benchmark_2._0.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Human_Benchmark_2._0.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserDataModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserDataModel>().Property(x => x.reactionTimesArray).HasConversion(
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x=>int.Parse(x)).ToArray()
                );
        }
    }
}
