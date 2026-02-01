using System;

namespace ei8.Cortex.Chat.Domain.Model
{
    /// <summary>
    /// Represents a String Info.
    /// </summary>
    public class StringInfo
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the string Value.
        /// </summary>
        public string Value { get; set; }
    }
}
