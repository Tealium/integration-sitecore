using System.Collections.Concurrent;

namespace Tealium.Sitecore.TagManagement.Settings
{
	using System.Collections.Generic;
	using System.Linq;
	using global::Sitecore.Data;
	using global::Sitecore.Exceptions;
	using Sc = global::Sitecore;

    public class SettingsProvider : ISettingsProvider
    {
        // Thank you Horizontal Integration for the update here to use ConcurrentDictionary
        private IDictionary<string, ITealiumSettings> _settings = new ConcurrentDictionary<string,ITealiumSettings>();

        /// <summary>
        /// Gets the tealium data provider.
        /// </summary>
        /// <value>
        /// The tealium data provider.
        /// </value>
        public string TealiumDataProvider
        {
            get
            {
                var setting = Sc.Configuration.Settings.GetSetting("Tealium.DataProvider", string.Empty);

                AssertSettingSpecified(setting, "Tealium.DataProvider");

                return setting;
            }
        }

        /// <summary>
        /// Gets the tealium mappings provider.
        /// </summary>
        /// <value>
        /// The tealium mappings provider.
        /// </value>
        public string TealiumMappingsProvider
        {
            get
            {
                var setting = Sc.Configuration.Settings.GetSetting("Tealium.MappingsProvider", string.Empty);

                AssertSettingSpecified(setting, "Tealium.MappingsProvider");

                return setting;
            }
        }

        /// <summary>
        /// Gets the tealium utag js URI format.
        /// </summary>
        /// <value>
        /// The tealium utag js URI format.
        /// </value>
        public string TealiumUtagJsUriFormat
        {
            get
            {
                var setting = Sc.Configuration.Settings.GetSetting("Tealium.Utag.Js.UriFormat", string.Empty);

                AssertSettingSpecified(setting, "Tealium.Utag.Js.UriFormat");

                return setting;
            }
        }

        /// <summary>
        /// Gets the tealium utag synchronize js URI format.
        /// </summary>
        /// <value>
        /// The tealium utag synchronize js URI format.
        /// </value>
        public string TealiumUtagSyncJsUriFormat
        {
            get
            {
                var setting = Sc.Configuration.Settings.GetSetting("Tealium.Utag.Sync.Js.UriFormat", string.Empty);

                AssertSettingSpecified(setting, "Tealium.Utag.Sync.Js.UriFormat");

                return setting;
            }
        }

        /// <summary>
        /// Gets the tealium tealium settings item identifier.
        /// </summary>
        /// <value>
        /// The tealium tealium settings item identifier.
        /// </value>
        public ID TealiumTealiumSettingsItemID
        {
            get
            {
                var setting = Sc.Configuration.Settings.GetSetting("Tealium.TealiumSettings.ItemID", string.Empty);

                return GetIdSetting(setting, "Tealium.TealiumSettings.ItemID");
            }
        }

        /// <summary>
        /// Gets the tealium utag data mapping settings item identifier.
        /// </summary>
        /// <value>
        /// The tealium utag data mapping settings item identifier.
        /// </value>
        public ID TealiumUtagDataMappingSettingsItemID
        {
            get
            {
                var setting = Sc.Configuration.Settings.GetSetting("Tealium.UtagDataMappingSettings.ItemID", string.Empty);

                return GetIdSetting(setting, "Tealium.UtagDataMappingSettings.ItemID");
            }
        }

        /// <summary>
        /// Gets the tealium settings.
        /// </summary>
        /// <returns></returns>
        public ITealiumSettings TealiumSettings
        {
            get
            {
	            var contextLanguage = TealiumFactory.SiteManager.ContextLanguageName;
	            var contextSiteName = TealiumFactory.SiteManager.ContextSiteName;
	            var key = contextSiteName + "_" + contextLanguage;

				if (!_settings.ContainsKey(key))
                {
                    var defaultSettingsItem = Sc.Context.Database.GetItem(TealiumTealiumSettingsItemID);

                    if (null != defaultSettingsItem)
                    {
                        var siteSpecificSettingItems = defaultSettingsItem.GetChildren().Select(x => new TealiumSettings(x)).ToArray();

                        _settings.Add(key, siteSpecificSettingItems.Any(x => x.WebsiteName == contextSiteName)
                            ? siteSpecificSettingItems.First(x => x.WebsiteName == contextSiteName)
                            : new TealiumSettings(defaultSettingsItem));
                    }
                    else
                    {
                        throw new ConfigurationException(string.Format("Tealium settings not published?  Cannot find value in database '{0}' for item '{1}'", Sc.Context.Database.Name, TealiumTealiumSettingsItemID));
                    }
                }

                return _settings[key];
            }
        }

        private static void AssertSettingSpecified(string setting, string settingName)
        {
            if (string.IsNullOrEmpty(setting))
            {
                throw new ConfigurationException(string.Format("Sitecore setting with name '{0}' is not specified.", settingName));
            }
        }

        private ID GetIdSetting(string setting, string settingName)
        {
            if (ID.IsID(setting))
            {
                return new ID(setting);
            }

            throw new ConfigurationException(string.Format("Sitecore setting with name '{0}' is not specified or doesn't represent an item ID.", settingName));
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            _settings.Clear();
        }
    }
}
