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

        public async Task<IEnumerable<Message>> GetMessagesAsync(string avatarUrl, DateTimeOffset? maxTimestamp, int? pageSize, IEnumerable<Guid> avatarIds = null, CancellationToken token = default)
        {
            var messageData = await this.messageQueryClient.GetMessagesAsync(
                        avatarUrl,
                        this.tokenProviderService.AccessToken,
                        maxTimestamp,
                        pageSize,
                        avatarIds,
                        token
                        );

            return messageData.Select(md => new Message()
            {
                Id = md.Id,
                Content = md.ContentString,
                Region = md.RegionTag,
                RegionId = md.RegionId,
                Sender = md.SenderTag,
                SenderId = md.SenderId,
                ExternalReferenceUrl = md.ExternalReferenceUrl,
                CreationTimestamp = md.CreationTimestamp,
                UnifiedLastModificationTimestamp = md.UnifiedLastModificationTimestamp,
                IsCurrentUserCreationAuthor = md.IsCurrentUserCreationAuthor
            });
        }
    }
}
