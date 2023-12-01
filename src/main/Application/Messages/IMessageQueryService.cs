using ei8.Cortex.Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Application.Messages
{
    public interface IMessageQueryService
    {
        Task<IEnumerable<Message>> GetMessagesAsync(DateTimeOffset? maxTimestamp = null, int? pageSize = null, CancellationToken token = default);
    }
}
