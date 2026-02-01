using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Nucleus.Client.Out;
using ei8.Cortex.Coding.Mirrors;
using neurUL.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application
{
    /// <summary>
    /// Provides functionality for retrieving Avatars.
    /// </summary>
    public class AvatarQueryService : IAvatarQueryService
    {
        private readonly IAvatarQueryClient avatarQueryClient;
        private readonly ITokenProviderService tokenProviderService;

        /// <summary>
        /// Constructs an AvatarQueryService.
        /// </summary>
        /// <param name="avatarQueryClient">IAvatarQueryClient to be used to connect to AvatarURL.</param>
        /// <param name="tokenProviderService">ITokenProviderService to retrieve tokens.</param>
        public AvatarQueryService(IAvatarQueryClient avatarQueryClient, ITokenProviderService tokenProviderService)
        {
            AssertionConcern.AssertArgumentNotNull(avatarQueryClient, nameof(avatarQueryClient));
            AssertionConcern.AssertArgumentNotNull(tokenProviderService, nameof(tokenProviderService));

            this.avatarQueryClient = avatarQueryClient;
            this.tokenProviderService = tokenProviderService;
        }

        /// <summary>
        /// Gets Avatars from the specified Avatar URL.
        /// </summary>
        /// <param name="avatarUrl">URL of the Avatar containing the Avatar neurons.</param>
        /// <param name="token">Cancellation token of the method.</param>
        /// <returns>An array of MirrorImageSeries of Avatars.</returns>
        public async Task<IEnumerable<IMirrorImageSeries<Avatar>>> GetAvatarsAsync(string avatarUrl, CancellationToken token = default)
        {
            var avatarData = await this.avatarQueryClient.GetAvatarsAsync(
                avatarUrl,
                this.tokenProviderService.AccessToken,
                token: token
            );

            return avatarData.Select(ad => ad.ToDomain());
        }

        /// <summary>
        /// Gets Avatars by their IDs from the specified Avatar URL.
        /// </summary>
        /// <param name="avatarUrl">URL of the Avatar containing the Avatar neurons.</param>
        /// <param name="ids">IDs of the Avatars to be retrieved.</param>
        /// <param name="token">Cancellation token of the method.</param>
        /// <returns>An array of MirrorImageSeries of matching Avatars.</returns>bb
        public Task<IEnumerable<IMirrorImageSeries<Avatar>>> GetAvatarsByIdsAsync(string avatarUrl, IEnumerable<Guid> ids, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
