using System;

namespace ei8.Cortex.Chat.Domain.Model
{
    /// <summary>
    /// Represents a Communicator.
    /// </summary>
    public class Communicator
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Avatar.
        /// </summary>
        public Avatar Avatar { get; set; }
    }
}
