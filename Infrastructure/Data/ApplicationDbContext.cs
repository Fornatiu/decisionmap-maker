using Domain.Aggregates.DecisionMapAggregate.Entities;
using Domain.Aggregates.QrMasterAggregate.Entities;
using Domain.Aggregates.UserAggregate.Entities;
using Infrastructure.EntityConfigurations;
using Infrastructure.EntityConfigurations.DecisionMapEntityTypeConfigurations;
using Infrastructure.EntityConfigurations.ProjectQrEntityTypeConfiguration;
using Infrastructure.EntityConfigurations.QrMasterEntityTypeConfigurations;
using Infrastructure.EntityConfigurations.UserAccountEntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<QrMaster> QrMaster { get; set; }
        public DbSet<DecisionMap> DecisionMap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserAccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccountCredentialsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccountInfoEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new QrMasterEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new DecisionMapEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectQrEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DependencyEdgeEntityTypeConfiguration());

        }
    }
}