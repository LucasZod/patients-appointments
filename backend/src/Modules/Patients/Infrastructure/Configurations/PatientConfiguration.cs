using Backend.Modules.Patients.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.Patients.Infrastructure.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasConversion(
                cpf => cpf.Value,
                value => new Cpf(value));

        builder.HasIndex(p => p.Cpf)
            .IsUnique();

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasMaxLength(20);

        builder.Property(p => p.CreatedAt)
            .IsRequired();
    }
}
