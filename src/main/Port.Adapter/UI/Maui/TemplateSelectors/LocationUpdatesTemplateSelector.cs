using ei8.Cortex.Chat.Domain.Model;
using System;
namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.TemplateSelectors
{
    public class LocationUpdatesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LocationTemplate { get; set; }
        public DataTemplate StatusTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            => item is LocationModel ? LocationTemplate : StatusTemplate;
    }
}

