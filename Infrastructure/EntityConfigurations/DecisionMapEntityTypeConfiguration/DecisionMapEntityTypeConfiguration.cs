using Domain.Aggregates.DecisionMapAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.EntityConfigurations.DecisionMapEntityTypeConfigurations
{
    class DecisionMapEntityTypeConfiguration : IEntityTypeConfiguration<DecisionMap>
    {
        public void Configure(EntityTypeBuilder<DecisionMap> p)
        {
           
            p.ToTable("DecisionMapProjects");
            p.HasKey(x => x.Id);

            p.Property(x => x.Name)
             .HasMaxLength(120)
             .IsRequired();

            p.Property(x => x.TimeStamp);

            p.HasMany(x => x.SelectedQrs)
             .WithOne()                    
             .HasForeignKey("ProjectId")   
             .OnDelete(DeleteBehavior.Cascade);

            p.HasMany(x => x.DMatrix)
             .WithOne()
             .HasForeignKey("ProjectId")
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
