using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Coding.Mirrors;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.TemplateSelectors
{
    /// <summary>
    /// Represents a MessageTemplateSelector.
    /// </summary>
    public class MessagesTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the Message data template.
        /// </summary>
        public DataTemplate MessageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Message data template for Messages with the same Senders as preceding Messages.
        /// </summary>
        public DataTemplate SameSenderMessageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Message data template for Messages sent by the current User.
        /// </summary>
        public DataTemplate CurrentUserMessageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Status data template.
        /// </summary>
        public DataTemplate StatusTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate result = null;
            var mirrorMessage = item as IMirrorImageSeries<Message>;
            if (mirrorMessage != null) {
                var itemsList = new List<IMirrorImageSeries<Message>>(
                    ((Microsoft.Maui.Controls.CollectionView)container).ItemsSource.Cast<IMirrorImageSeries<Message>>()
                );
                var currIndex = itemsList.IndexOf(mirrorMessage);
                var sameMessageSender = false;
                if (currIndex > 0)
                {
                    var precedingItem = itemsList[currIndex - 1];
                    var currentSendersAvatarIds = mirrorMessage.First().Senders.Select(ms => ms.Avatar.Id.ToString());
                    var precedingSendersAvatarIds = precedingItem.First().Senders.Select(ps => ps.Avatar.Id.ToString());
                    if (precedingSendersAvatarIds.Intersect(currentSendersAvatarIds).Any())
                        sameMessageSender = true;
                }
                result = mirrorMessage.First().IsCurrentUserCreationAuthor ?
                    this.CurrentUserMessageTemplate :
                    (
                        !sameMessageSender ?
                            this.MessageTemplate :
                            this.SameSenderMessageTemplate
                    );
            }
            else
                result = this.StatusTemplate;

            return result;
        }
    }
}

