using Backend.Modules.Samples.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.Samples.Infrastructure.Configurations;

public class SampleConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> builder)
    {
        builder.ToTable("samples");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.ServiceOrderId)
            .IsRequired();

        builder.Property(s => s.TubeType)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(s => s.CollectedAt)
            .IsRequired();

        builder.Property(s => s.ReviewedAt)
            .IsRequired(false);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.OwnsOne(s => s.RejectionReason, rr =>
        {
            rr.Property(r => r.Code)
                .HasColumnName("rejection_reason")
                .HasMaxLength(50)
                .HasConversion<string>();

            rr.Property(r => r.Notes)
                .HasColumnName("rejection_notes");
        });

        builder.HasIndex(s => s.ServiceOrderId);
        builder.HasIndex(s => s.Status);
    }
}
