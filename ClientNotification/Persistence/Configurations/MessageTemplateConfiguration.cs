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


            builder.HasData(
                new MessageTemplate
                {
                    Id = 1,
                    Name = "Reminder",
                    Message = "{Creditnumber}<br>Dear {Name}<br><br><br>Please pay by {dueDate}<br>The amount {amount}<br><br><br>Greetings Vexcash"
                },

                new MessageTemplate
                {
                    Id  = 2,
                    Name = "Cancellation",
                    Message = "Dear {Name}<br>Thank you for your recent application for a VEXCASH credit. Unfortunately, you do not meet our current criteria for credit approval. <br>Your request for {Amount} euro credit was declined<br>If you feel that you have information that will make a difference in these two considerations, please write to us."
                }
                ); 
        }
    }
}
