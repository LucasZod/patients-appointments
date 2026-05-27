using Backend.Modules.ServiceOrders.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.ServiceOrders.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ServiceOrderId)
            .IsRequired();

        builder.Property(oi => oi.ExamCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(oi => oi.ExamName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(oi => oi.TubeType)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(oi => oi.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(oi => oi.CreatedAt)
            .IsRequired();
    }
}
