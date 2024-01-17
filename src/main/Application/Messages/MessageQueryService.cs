using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Nucleus.Client.Out;
using neurUL.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application.Messages
{
    public class MessageQueryService : IMessageQueryService
    {
        private readonly IMessageQueryClient messageQueryClient;
        private readonly ITokenProviderService tokenProviderService;

        public MessageQueryService(IMessageQueryClient messageQueryClient, ITokenProviderService tokenProviderService)
        {
            AssertionConcern.AssertArgumentNotNull(messageQueryClient, nameof(messageQueryClient));
            AssertionConcern.AssertArgumentNotNull(tokenProviderService, nameof(tokenProviderService));

            this.messageQueryClient = messageQueryClient;
            this.tokenProviderService = tokenProviderService;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string avatarUrl, DateTimeOffset? maxTimestamp = null, int? pageSize = null, CancellationToken token = default)
        {
            var messageData = await this.messageQueryClient.GetMessagesAsync(
                        avatarUrl,
                        this.tokenProviderService.AccessToken
                        );

            return messageData.Select(md => new Message()
            {
                Id = md.Id,
                Content = md.Content,
                Region = md.Region,
                RegionId = md.RegionId,
                Sender = md.Sender,
                SenderId = md.SenderId,
                CreationTimestamp = md.CreationTimestamp,
                UnifiedLastModificationTimestamp = md.UnifiedLastModificationTimestamp,
                IsCurrentUserCreationAuthor = md.IsCurrentUserCreationAuthor
            });
        }
    }
}
