using System;

namespace ei8.Cortex.Chat.Domain.Model;

public record Message()
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public string Sender { get; set; }

    public Guid SenderId { get; set; }

    public string Region { get; set; }

    public Guid? RegionId { get; set; }

    public string ExternalReferenceUrl { get; set; }

    public DateTimeOffset? CreationTimestamp { get; set; }

    public DateTimeOffset? UnifiedLastModificationTimestamp { get; set; }

    public bool IsCurrentUserCreationAuthor { get; set; }
}