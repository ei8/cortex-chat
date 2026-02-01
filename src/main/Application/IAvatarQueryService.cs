using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Coding.Mirrors;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application
{
    /// <summary>
    /// Provides functionality for retrieving Avatars.
    /// </summary>
    public interface IAvatarQueryService
    {
        /// <summary>
        /// Gets Avatars from the specified Avatar URL.
        /// </summary>
        /// <param name="avatarUrl">URL of the Avatar containing the Avatar neurons.</param>
        /// <param name="token">Cancellation token of the method.</param>
        /// <returns>An array of MirrorImageSeries of Avatars.</returns>
        Task<IEnumerable<IMirrorImageSeries<Avatar>>> GetAvatarsAsync(string avatarUrl, CancellationToken token = default);

        /// <summary>
        /// Gets Avatars by their IDs from the specified Avatar URL.
        /// </summary>
        /// <param name="avatarUrl">URL of the Avatar containing the Avatar neurons.</param>
        /// <param name="ids">IDs of the Avatars to be retrieved.</param>
        /// <param name="token">Cancellation token of the method.</param>
        /// <returns>An array of MirrorImageSeries of matching Avatars.</returns>
        Task<IEnumerable<IMirrorImageSeries<Avatar>>> GetAvatarsByIdsAsync(string avatarUrl, IEnumerable<Guid> ids, CancellationToken token = default);
    }
}
