using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Coding.Mirrors;
using System;
namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.TemplateSelectors
{
    public class MessagesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MessageTemplate { get; set; }
        public DataTemplate SameSenderMessageTemplate { get; set; }
        public DataTemplate CurrentUserMessageTemplate { get; set; }
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
                    if (precedingItem.First().Senders.IntersectBy(mirrorMessage.First().Senders.Select(ms => ms.Id), c => c.Id).Any())
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

