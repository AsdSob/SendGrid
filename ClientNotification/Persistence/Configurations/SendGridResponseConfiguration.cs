using ClientNotification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientNotification.Persistence.Configurations
{
    public class SendGridResponseConfiguration : IEntityTypeConfiguration<SendGridResponse>
    {
        public void Configure(EntityTypeBuilder<SendGridResponse> builder)
        {
            builder.ToTable("SendGridResponses");
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Created)
                .IsRequired();
            builder.Property(x => x.Response)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.Template)
                .WithMany()
                .HasForeignKey(x => x.TemplateId);
        }
    }
}
