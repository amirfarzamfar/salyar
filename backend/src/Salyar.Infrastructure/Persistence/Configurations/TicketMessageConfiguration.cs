using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salyar.Domain.Entities;

namespace Salyar.Infrastructure.Persistence.Configurations;

public class TicketMessageConfiguration : IEntityTypeConfiguration<TicketMessage>
{
    public void Configure(EntityTypeBuilder<TicketMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.MessageBody)
            .IsRequired();

        builder.HasMany(m => m.Attachments)
            .WithOne(a => a.TicketMessage)
            .HasForeignKey(a => a.TicketMessageId)
            .OnDelete(DeleteBehavior.ClientCascade); // Optional: behavior for message attachments
    }
}
