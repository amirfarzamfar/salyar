using Salyar.Domain.Common;

namespace Salyar.Domain.Entities;

public class TicketAttachment : BaseEntity
{
    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }

    public int? TicketMessageId { get; set; }
    public TicketMessage? TicketMessage { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty; // Or stored in DB
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
}
