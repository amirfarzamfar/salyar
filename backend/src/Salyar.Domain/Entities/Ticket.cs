using Salyar.Domain.Common;
using Salyar.Domain.Enums;

namespace Salyar.Domain.Entities;

public class Ticket : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; // Initial description
    public string TrackingCode { get; set; } = string.Empty; // For user reference

    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketCategory Category { get; set; }

    // User who created the ticket (Seeker or Provider)
    public string CreatedByUserId { get; set; } = string.Empty;

    // Admin/Department assigned (optional)
    public string? AssignedToUserId { get; set; }

    public ICollection<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
    public ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
}
