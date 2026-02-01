using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Coding.Mirrors;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application.Messages
{
    /// <summary>
    /// Provides functionality for retrieving Messages.
    /// </summary>
    public interface IMessageQueryService
    {
        /// <summary>
        /// Gets Messages from the specified Avatar URL.
        /// </summary>
        /// <param name="avatarUrl">URL of the Avatar containing the Message neurons.</param>
        /// <param name="maxTimestamp">The max timestamp of the Messages.</param>
        /// <param name="pageSize">The number of Messages in each result set.</param>
        /// <param name="avatarIds">The Avatar IDs of the Messages' Senders.</param>
        /// <param name="token">Cancellation token of the method.</param>
        /// <returns>An array of MirrorImageSeries of Messages.</returns>
        Task<IEnumerable<IMirrorImageSeries<Message>>> GetMessagesAsync(string avatarUrl, DateTimeOffset? maxTimestamp, int? pageSize, IEnumerable<Guid> avatarIds = null, CancellationToken token = default);
    }
}
