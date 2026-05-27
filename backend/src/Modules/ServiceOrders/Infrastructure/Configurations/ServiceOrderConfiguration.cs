using Backend.Modules.ServiceOrders.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.ServiceOrders.Infrastructure.Configurations;

public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
{
    public void Configure(EntityTypeBuilder<ServiceOrder> builder)
    {
        builder.ToTable("service_orders");

        builder.HasKey(so => so.Id);

        builder.Property(so => so.PatientId)
            .IsRequired();

        builder.Property(so => so.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(so => so.Priority)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(so => so.CalledAt)
            .IsRequired(false);

        builder.Property(so => so.FinishedAt)
            .IsRequired(false);

        builder.Property(so => so.CreatedAt)
            .IsRequired();

        builder.HasMany(so => so.Items)
            .WithOne()
            .HasForeignKey(oi => oi.ServiceOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(ServiceOrder.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(so => so.Status);
        builder.HasIndex(so => so.PatientId);
    }
}
