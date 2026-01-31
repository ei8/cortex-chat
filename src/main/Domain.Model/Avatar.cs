using ei8.Cortex.Coding.Mirrors;
using System;

namespace ei8.Cortex.Chat.Domain.Model
{
    public class Avatar : IMirrorImage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public MirrorInfo Mirror { get; set; }
    }
}
