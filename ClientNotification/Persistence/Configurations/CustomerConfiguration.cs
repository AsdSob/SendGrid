using ClientNotification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientNotification.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(x => x.CreditNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.EMail)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(x => x.Amount)
                .IsRequired();
            builder.Property(x => x.DueDate)
                .IsRequired();

            builder.HasIndex(x => x.CreditNumber)
                .IsUnique();
            builder.HasIndex(x => x.EMail)
                .IsUnique();
        }
    }
}
