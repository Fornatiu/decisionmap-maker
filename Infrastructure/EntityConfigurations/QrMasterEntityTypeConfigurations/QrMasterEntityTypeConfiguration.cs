using Domain.Aggregates.QrMasterAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.QrMasterEntityTypeConfigurations
{
    public class QrMasterEntityTypeConfiguration : IEntityTypeConfiguration<QrMaster>
    {
        public void Configure(EntityTypeBuilder<QrMaster> builder)
        {
            builder.ToTable("QrMasters");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Dimension)
                .HasConversion<string>()
                .IsRequired();

        }
    }
}
