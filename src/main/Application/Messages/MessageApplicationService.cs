using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Nucleus.Client.In;
using neurUL.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application.Messages
{
    public class MessageApplicationService : IMessageApplicationService
    {
        private readonly IMessageClient messageClient;
        private readonly ITokenProviderService tokenProviderService;

        public MessageApplicationService(IMessageClient messageClient, ITokenProviderService tokenProviderService)
        {
            AssertionConcern.AssertArgumentNotNull(messageClient, nameof(messageClient));
            AssertionConcern.AssertArgumentNotNull(tokenProviderService, nameof(tokenProviderService));

            this.messageClient = messageClient;
            this.tokenProviderService = tokenProviderService;
        }

        public async Task SendMessageAsync(string avatarUrl, string content, string regionId, IEnumerable<string> recipientAvatarIds, CancellationToken token = default)
        {
            await this.messageClient.CreateMessage(
                avatarUrl,
                this.tokenProviderService.AccessToken,
                Guid.NewGuid().ToString(),
                content,
                regionId,
                string.Empty,
                recipientAvatarIds,                
                token
            );
        }
    }
}
