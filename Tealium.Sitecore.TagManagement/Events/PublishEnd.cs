using System;

namespace Tealium.Sitecore.TagManagement.Events
{
    public class PublishEnd
    {
        public void Refresh(object sender, EventArgs args)
        {
            TealiumFactory.SettingsProvider.Refresh();
            TealiumFactory.MappingsProvider.Refresh();
        }
    }
}
