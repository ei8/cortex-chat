using ei8.Cortex.Chat.Domain.Model;
using System;
using System.Linq;

namespace ei8.Cortex.Chat.Port.Adapter.IO.Process.Services
{
    public class UrlService : IUrlService
    {
        public string AvatarUrl { get; set; }

        public string Authority 
        { 
            get
            {
                var uri = new Uri(this.AvatarUrl);
                return $"{uri.Scheme}://login.{uri.Host}";
            }
        }

        public string AvatarName
        {
            get
            {
                var uri = new Uri(this.AvatarUrl);
                return uri.Segments.ElementAtOrDefault(1);
            }
        }
    }
}
