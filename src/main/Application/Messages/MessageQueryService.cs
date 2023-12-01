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
        private readonly IUrlService urlService;
        private readonly IMessageQueryClient messageQueryClient;
        private readonly ITokenProviderService tokenProviderService;

        public MessageQueryService(IUrlService urlService, IMessageQueryClient messageQueryClient, ITokenProviderService tokenProviderService)
        {
            AssertionConcern.AssertArgumentNotNull(urlService, nameof(urlService));
            AssertionConcern.AssertArgumentNotNull(messageQueryClient, nameof(messageQueryClient));
            AssertionConcern.AssertArgumentNotNull(tokenProviderService, nameof(tokenProviderService));

            this.urlService = urlService;
            this.messageQueryClient = messageQueryClient;
            this.tokenProviderService = tokenProviderService;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(DateTimeOffset? maxTimestamp = null, int? pageSize = null, CancellationToken token = default)
        {
            var messageData = await this.messageQueryClient.GetMessagesAsync(
                        this.urlService.AvatarUrl + "/",
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
