using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application.Messages
{
    public interface IMessageApplicationService
    {
        Task SendMessageAsync(string avatarUrl, string content, string regionId, IEnumerable<string> recipientAvatarIds, CancellationToken token = default);
    }
}
