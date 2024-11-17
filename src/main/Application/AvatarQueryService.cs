using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Nucleus.Client.Out;
using neurUL.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application
{
    public class AvatarQueryService : IAvatarQueryService
    {
        private readonly IAvatarQueryClient avatarQueryClient;
        private readonly ITokenProviderService tokenProviderService;

        public AvatarQueryService(IAvatarQueryClient avatarQueryClient, ITokenProviderService tokenProviderService)
        {
            AssertionConcern.AssertArgumentNotNull(avatarQueryClient, nameof(avatarQueryClient));
            AssertionConcern.AssertArgumentNotNull(tokenProviderService, nameof(tokenProviderService));

            this.avatarQueryClient = avatarQueryClient;
            this.tokenProviderService = tokenProviderService;
        }

        public async Task<IEnumerable<Avatar>> GetAvatarsAsync(string avatarUrl, CancellationToken token = default)
        {
            var avatarData = await this.avatarQueryClient.GetAvatarsAsync(
                avatarUrl,
                this.tokenProviderService.AccessToken,
                token: token
            );

            return avatarData.Select(ad => new Avatar()
            {
                Id = ad.Id,
                Name = ad.Name,
                Url = ad.Url,
                ExternalReferenceUrl = ad.ExternalReferenceUrl
            }); ;
        }

        public Task<IEnumerable<Avatar>> GetAvatarsByIdsAsync(string avatarUrl, IEnumerable<Guid> ids, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
