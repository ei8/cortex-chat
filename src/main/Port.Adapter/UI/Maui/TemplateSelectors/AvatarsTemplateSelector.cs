using ei8.Cortex.Chat.Domain.Model;
using System;
namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.TemplateSelectors
{
    public class AvatarsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AvatarTemplate { get; set; }
        
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return this.AvatarTemplate;
        }
    }
}

