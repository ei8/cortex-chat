using ei8.Cortex.Coding.Mirrors;
using ei8.Cortex.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ei8.Cortex.Chat.Domain.Model;

public record Message() : IMirrorImage
{
    public Guid Id { get; set; }

    public StringInfo Content { get; set; }

    public IEnumerable<Communicator> Senders { get; set; }

    public string SendersNames => string.Join(", ", this.Senders.Select(s => s.Avatar.Name));

    public IEnumerable<Communicator> Recipients { get; set; }

    public NeuronInfo Region { get; set; }

    public DateTimeOffset? CreationTimestamp { get; set; }

    public DateTimeOffset? UnifiedLastModificationTimestamp { get; set; }

    public bool IsCurrentUserCreationAuthor { get; set; }

    public string Url { get; set; } 

    public MirrorInfo Mirror { get; set; }
}