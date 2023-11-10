using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Domain.Model
{
    public interface IUrlService
    {
        string AvatarUrl { get; set; }

        string Authority { get; }

        string AvatarName { get; }
    }
}
