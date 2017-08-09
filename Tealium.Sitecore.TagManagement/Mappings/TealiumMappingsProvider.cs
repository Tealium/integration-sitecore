using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Tealium.Sitecore.TagManagement.Settings;
using Tealium.Sitecore.TagManagement.Site;
using Context = Sitecore.Context;

namespace Tealium.Sitecore.TagManagement.Mappings
{
    public class TealiumMappingsProvider : ITealiumMappingsProvider
    {
	    public ISiteManager SiteManager;
	    public ISettingsProvider SettingsProvider { get; private set; }

        //HI CUSTOMIZATION -- MODIFIED TO CONCURRENTDICTIONARY
        public IDictionary<string, IMapping> _mapping = new ConcurrentDictionary<string, IMapping>();

	    /// <summary>
	    /// Initializes a new instance of the <see cref="TealiumMappingsProvider"/> class.
	    /// </summary>
	    /// <param name="settingsProvider">The settings provider.</param>
	    public TealiumMappingsProvider(ISettingsProvider settingsProvider)
        {
	        this.SettingsProvider = settingsProvider;
        }

	    /// <summary>
        /// Gets the mapping.
        /// </summary>
        /// <returns></returns>
        public IMapping Mapping
        {
            get
            {
	            var contextLanguage = TealiumFactory.SiteManager.ContextLanguageName;
	            var contextSiteName = TealiumFactory.SiteManager.ContextSiteName;

	            var uniqueKey = contextSiteName + "_" + contextLanguage;

				if (!_mapping.ContainsKey(uniqueKey))
                {
                    var mappingSettingsItem = Context.Database.GetItem(SettingsProvider.TealiumUtagDataMappingSettingsItemID);
					var siteSpecificMapping = mappingSettingsItem.GetChildren()
						.Where(x => x.TemplateID == Const.TemplateIDs.UtagDataSiteMappingSettings)
						.FirstOrDefault(x => string.Equals(x[Const.FieldIDs.MappingSettings.WebsiteName], contextSiteName, StringComparison.InvariantCultureIgnoreCase));

	                if (siteSpecificMapping != null)
	                {
		                mappingSettingsItem = siteSpecificMapping;
	                }

	                _mapping.Add(uniqueKey, new Mapping(mappingSettingsItem)); 
                }

				return _mapping[uniqueKey];
            }
        }

        public void Refresh()
        {
            _mapping.Clear();
        }
    }
}
