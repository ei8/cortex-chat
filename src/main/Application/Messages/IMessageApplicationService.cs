using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application.Messages
{
    public interface IMessageApplicationService
    {
        Task SendMessageAsync(string content, string regionId, CancellationToken token = default);
    }
}
