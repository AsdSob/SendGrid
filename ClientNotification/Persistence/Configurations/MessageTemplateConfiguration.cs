using ClientNotification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientNotification.Persistence.Configurations
{
    public class MessageTemplateConfiguration : IEntityTypeConfiguration<MessageTemplate>
    {
        public void Configure(EntityTypeBuilder<MessageTemplate> builder)
        {
            builder.ToTable("MessageTemplates");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
