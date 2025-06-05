using Domain.Aggregates.DecisionMapAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class DependencyEdgeEntityTypeConfiguration : IEntityTypeConfiguration<DependencyEdge>
{
    public void Configure(EntityTypeBuilder<DependencyEdge> e)
    {
        e.ToTable("DependencyEdges");
        e.HasKey(edge => edge.Id);

        e.Property<Guid>("ProjectId").IsRequired();

        e.HasOne<ProjectQr>()
         .WithMany()
         .HasForeignKey(edge => edge.FromQrId)
         .OnDelete(DeleteBehavior.Restrict);

        e.HasOne<ProjectQr>()
         .WithMany()
         .HasForeignKey(edge => edge.ToQrId)
         .OnDelete(DeleteBehavior.Restrict);

        e.HasIndex("ProjectId", nameof(DependencyEdge.FromQrId), nameof(DependencyEdge.ToQrId))
         .IsUnique();

        e.Property(edge => edge.Effect)
         .HasConversion<string>()
         .IsRequired();
    }
}
