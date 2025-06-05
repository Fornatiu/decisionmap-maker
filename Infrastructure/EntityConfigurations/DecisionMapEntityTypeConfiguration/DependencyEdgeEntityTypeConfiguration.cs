using Domain.Aggregates.DecisionMapAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class DependencyEdgeEntityTypeConfiguration : IEntityTypeConfiguration<DependencyEdge>
{
    public void Configure(EntityTypeBuilder<DependencyEdge> e)
    {
        // ---------------- Table & PK ----------------
        e.ToTable("DependencyEdges");
        e.HasKey(edge => edge.Id);

        // ---------------- Foreign keys ----------------
        // Shadow FK back to DecisionMapProject (defined in root config)
        e.Property<Guid>("ProjectId").IsRequired();

        // Edge endpoints reference ProjectQr.Id (not QrMasterId!)
        e.HasOne<ProjectQr>()
         .WithMany()
         .HasForeignKey(edge => edge.FromQrId)
         .OnDelete(DeleteBehavior.Restrict);

        e.HasOne<ProjectQr>()
         .WithMany()
         .HasForeignKey(edge => edge.ToQrId)
         .OnDelete(DeleteBehavior.Restrict);

        // ---------------- Constraints ----------------
        // One edge per ordered pair inside the same project
        e.HasIndex("ProjectId", nameof(DependencyEdge.FromQrId), nameof(DependencyEdge.ToQrId))
         .IsUnique();

        // ---------------- Enum mapping ----------------
        e.Property(edge => edge.Effect)
         .HasConversion<string>()
         .IsRequired();
    }
}
