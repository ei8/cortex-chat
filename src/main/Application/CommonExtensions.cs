using ei8.Cortex.Chat.Common;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Coding.Mirrors;
using System.Linq;

namespace ei8.Cortex.Chat.Application
{
    internal static class CommonExtensions
    {
        internal static MirrorImageSeries<Message> ToDomain(this IMirrorImageSeries<MessageResult> series) =>
            new(
                series.Select(
                    mr => mr.ToDomain()
                )
            );

        internal static MirrorImageSeries<Avatar> ToDomain(this IMirrorImageSeries<Common.AvatarInfo> series) =>
            new(
                series.Select(
                    ai => ai.ToDomain()
                )
            );

        internal static Message ToDomain(this Common.MessageResult messageResult) =>
            new() 
            {
                Id = messageResult.Id,
                Content = messageResult.Content?.ToDomain(),
                Region = messageResult.Region?.ToLibraryCommon(),
                Senders = messageResult.Senders?.Select(s => s.ToDomain()),
                Recipients = messageResult.Recipients?.Select(s => s.ToDomain()),
                Mirror = messageResult.Mirror?.Clone(),
                CreationTimestamp = messageResult.CreationTimestamp,
                UnifiedLastModificationTimestamp = messageResult.UnifiedLastModificationTimestamp,
                IsCurrentUserCreationAuthor = messageResult.IsCurrentUserCreationAuthor
            };

        internal static Domain.Model.StringInfo ToDomain(this Common.StringInfo stringInfo) =>
            new()
            {
                Id = stringInfo.Id,
                Value = stringInfo.Value,
            };

        internal static Communicator ToDomain(this Common.CommunicatorInfo communicatorInfo) =>
            new()
            {
                Id = communicatorInfo.Id,
                Avatar = communicatorInfo.Avatar.ToDomain()
            };

        internal static Library.Common.NeuronInfo ToLibraryCommon(this Common.NeuronInfo neuronInfo) =>
            new()
            {
                Id = neuronInfo.Id.ToString(),
                Tag = neuronInfo.Tag
            };

        internal static Avatar ToDomain(this Common.AvatarInfo avatarInfo) =>
            new()
            {
                Id = avatarInfo.Id,
                Name = avatarInfo.Name,
                Url = avatarInfo.Url,
                Mirror = avatarInfo.Mirror.Clone()
            };

        internal static MirrorInfo Clone(this MirrorInfo mirrorInfo) =>
            new()
            {
                Url = mirrorInfo.Url,
                IsValid = mirrorInfo.IsValid,
                ValidationTimestamp = mirrorInfo.ValidationTimestamp
            };
    }
}