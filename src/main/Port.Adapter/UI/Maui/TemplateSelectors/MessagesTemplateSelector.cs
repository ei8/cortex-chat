using ei8.Cortex.Chat.Domain.Model;
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
            var itemsList = new List<Message>(((Microsoft.Maui.Controls.CollectionView)container).ItemsSource.Cast<Message>());
            var message = (Message)item;
            var currIndex = itemsList.IndexOf(message);
            var sameMessageSender = false;
            if (currIndex > 0) 
            {
                var precedingItem = (Message) itemsList[currIndex - 1];
                if (precedingItem.SenderId == message.SenderId) 
                    sameMessageSender = true;
            }
            return item is Message ? 
                ( message.IsCurrentUserCreationAuthor ?
                    this.CurrentUserMessageTemplate :
                    (
                        !sameMessageSender ? 
                            MessageTemplate : 
                            SameSenderMessageTemplate
                    )
                ) :
                StatusTemplate;
        }
    }
}

