using Human_Benchmark_2._0.Models.DataModels;
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
            modelBuilder.Entity<UserDataModel>().Property(x => x.reactionTimesArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                );
            modelBuilder.Entity<UserDataModel>().Property(x => x.avgTimeScoreArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray()
                );
            modelBuilder.Entity<UserDataModel>().Property(x => x.memoryNumbersScoreArray).HasConversion
                (
                x => string.Join(',', x),
                x=>x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                );
            modelBuilder.Entity<UserDataModel>().Property(x => x.memoryWordsScoreArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                ); 
            modelBuilder.Entity<UserDataModel>().Property(x => x.reverseWordsScoreArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                ); 
            modelBuilder.Entity<UserDataModel>().Property(x => x.reverseNumbersScoreArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                );
            modelBuilder.Entity<UserDataModel>().Property(x => x.blocksScoreArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                );
            modelBuilder.Entity<UserDataModel>().Property(x => x.audioReactionAvgTimeArray).HasConversion
                (
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                );
        }
    }
}
