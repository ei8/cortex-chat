using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Coding.Mirrors;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application
{
    public interface IAvatarQueryService
    {
        Task<IEnumerable<IMirrorImageSeries<Avatar>>> GetAvatarsAsync(string avatarUrl, CancellationToken token = default);

        Task<IEnumerable<IMirrorImageSeries<Avatar>>> GetAvatarsByIdsAsync(string avatarUrl, IEnumerable<Guid> ids, CancellationToken token = default);
    }
}
