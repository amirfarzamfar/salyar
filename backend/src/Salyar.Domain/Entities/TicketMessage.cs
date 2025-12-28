using Salyar.Domain.Common;

namespace Salyar.Domain.Entities;

public class TicketMessage : BaseEntity
{
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public string SenderUserId { get; set; } = string.Empty;
    public bool IsAdminReply { get; set; }
    public string MessageBody { get; set; } = string.Empty;
    public bool IsInternalNote { get; set; } // For admins only

    public ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
}
