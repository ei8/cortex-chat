using ei8.Cortex.Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application
{
    public interface IAvatarQueryService
    {
        Task<IEnumerable<Avatar>> GetAvatarsAsync(string avatarUrl, CancellationToken token = default);

        Task<IEnumerable<Avatar>> GetAvatarsByIdsAsync(string avatarUrl, IEnumerable<Guid> ids, CancellationToken token = default);
    }
}
