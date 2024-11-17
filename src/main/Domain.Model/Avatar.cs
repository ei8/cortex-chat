using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Domain.Model
{
    public class Avatar
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ExternalReferenceUrl { get; set; }

        public string Url { get; set; }
    }
}
