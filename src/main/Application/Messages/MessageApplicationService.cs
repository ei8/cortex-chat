using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Nucleus.Client.In;
using ei8.Cortex.Chat.Nucleus.Client.Out;
using ei8.Cortex.Library.Common;
using neurUL.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task SendMessageAsync(string avatarUrl, string content, string regionId, CancellationToken token = default)
        {
            await this.messageClient.CreateMessage(
                avatarUrl,
                Guid.NewGuid().ToString(),
                content,
                regionId,
                this.tokenProviderService.AccessToken
                );
        }
    }
}
