using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.DecisionMapAggregate.Entities;

namespace Infrastructure.EntityConfigurations.ProjectQrEntityTypeConfiguration
{
    class ProjectQrEntityTypeConfiguration : IEntityTypeConfiguration<ProjectQr>
    {
        public void Configure(EntityTypeBuilder<ProjectQr> builder)
        {
            builder.ToTable("ProjectQrs");
            builder.HasKey(x => x.Id);

            builder.HasIndex("ProjectId", nameof(ProjectQr.QrMasterId)).IsUnique();

            //builder.Property(x => x.Id)
            //       .HasDefaultValueSql("NEWID()")
            //       .ValueGeneratedOnAdd();

            builder.Property(x => x.ImpactLevel)
             .HasConversion<string>()          
             .IsRequired();

            builder.Property(x => x.Dimension)
             .HasConversion<string>()
             .IsRequired();
        }
    }
}
