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

namespace ei8.Cortex.Chat.Application.Messages
{
    /// <summary>
    /// Provides functionality for retrieving Messages.
    /// </summary>
    public class MessageQueryService : IMessageQueryService
    {
        private readonly IMessageQueryClient messageQueryClient;
        private readonly ITokenProviderService tokenProviderService;

        /// <summary>
        /// Constructs a MessageQueryService.
        /// </summary>
        /// <param name="messageQueryClient">IMessageQueryClient to be used to connect to AvatarURL.</param>
        /// <param name="tokenProviderService">ITokenProviderService to retrieve tokens.</param>
        public MessageQueryService(IMessageQueryClient messageQueryClient, ITokenProviderService tokenProviderService)
        {
            AssertionConcern.AssertArgumentNotNull(messageQueryClient, nameof(messageQueryClient));
            AssertionConcern.AssertArgumentNotNull(tokenProviderService, nameof(tokenProviderService));

            this.messageQueryClient = messageQueryClient;
            this.tokenProviderService = tokenProviderService;
        }

        /// <summary>
        /// Gets Messages from the specified Avatar URL.
        /// </summary>
        /// <param name="avatarUrl">URL of the Avatar containing the Message neurons.</param>
        /// <param name="maxTimestamp">The max timestamp of the Messages.</param>
        /// <param name="pageSize">The number of Messages in each result set.</param>
        /// <param name="avatarIds">The Avatar IDs of the Messages' Senders.</param>
        /// <param name="token">Cancellation token of the method.</param>
        /// <returns>An array of MirrorImageSeries of Messages.</returns>
        public async Task<IEnumerable<IMirrorImageSeries<Message>>> GetMessagesAsync(string avatarUrl, DateTimeOffset? maxTimestamp, int? pageSize, IEnumerable<Guid> avatarIds = null, CancellationToken token = default)
        {
            var messageData = await this.messageQueryClient.GetMessagesAsync(
                avatarUrl,
                this.tokenProviderService.AccessToken,
                maxTimestamp,
                pageSize,
                true,
                avatarIds,
                token
            );

            return messageData.Select(
                md => md.ToDomain()
            );
        }
    }
}
