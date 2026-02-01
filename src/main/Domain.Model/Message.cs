using ei8.Cortex.Coding.Mirrors;
using ei8.Cortex.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ei8.Cortex.Chat.Domain.Model;

/// <summary>
/// Represents a Message.
/// </summary>
public record Message() : IMirrorImage
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Content.
    /// </summary>
    public StringInfo Content { get; set; }

    /// <summary>
    /// Gets or sets the Senders.
    /// </summary>
    public IEnumerable<Communicator> Senders { get; set; }

    /// <summary>
    /// Gets the names of the Senders separated by commas.
    /// </summary>
    public string SendersNames => string.Join(", ", this.Senders.Select(s => s.Avatar.Name));

    /// <summary>
    /// Gets or sets the Recipients.
    /// </summary>
    public IEnumerable<Communicator> Recipients { get; set; }

    /// <summary>
    ///  Gets or sets the Region.
    /// </summary>
    public NeuronInfo Region { get; set; }

    /// <summary>
    /// Gets or sets the Creation timestamp.
    /// </summary>
    public DateTimeOffset? CreationTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the Unified Last Modification Timestamp.
    /// </summary>
    public DateTimeOffset? UnifiedLastModificationTimestamp { get; set; }

    /// <summary>
    /// Gets or sets an indication whether the current user is the author.
    /// </summary>
    public bool IsCurrentUserCreationAuthor { get; set; }

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    public string Url { get; set; } 

    /// <summary>
    /// Gets or sets the Mirror Info.
    /// </summary>
    public MirrorInfo Mirror { get; set; }
}