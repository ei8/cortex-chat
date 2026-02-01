using ei8.Cortex.Coding.Mirrors;
using System;

namespace ei8.Cortex.Chat.Domain.Model
{
    /// <summary>
    /// Represents an Avatar.
    /// </summary>
    public class Avatar : IMirrorImage
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the Mirror Info.
        /// </summary>
        public MirrorInfo Mirror { get; set; }
    }
}
