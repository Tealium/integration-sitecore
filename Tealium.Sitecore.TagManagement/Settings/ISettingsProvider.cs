namespace Tealium.Sitecore.TagManagement.Settings
{
	using global::Sitecore.Data;

    public interface ISettingsProvider : IRefreshable
    {
        /// <summary>
        /// Gets the tealium data provider.
        /// </summary>
        /// <value>
        /// The tealium data provider.
        /// </value>
        string TealiumDataProvider { get; }

        /// <summary>
        /// Gets the tealium mappings provider.
        /// </summary>
        /// <value>
        /// The tealium mappings provider.
        /// </value>
        string TealiumMappingsProvider { get; }

        /// <summary>
        /// Gets the tealium utag js URI format.
        /// </summary>
        /// <value>
        /// The tealium utag js URI format.
        /// </value>
        string TealiumUtagJsUriFormat { get; }

        /// <summary>
        /// Gets the tealium utag synchronize js URI format.
        /// </summary>
        /// <value>
        /// The tealium utag synchronize js URI format.
        /// </value>
        string TealiumUtagSyncJsUriFormat { get; }

        /// <summary>
        /// Gets the tealium tealium settings item identifier.
        /// </summary>
        /// <value>
        /// The tealium tealium settings item identifier.
        /// </value>
        ID TealiumTealiumSettingsItemID { get; }

        /// <summary>
        /// Gets the tealium utag data mapping settings item identifier.
        /// </summary>
        /// <value>
        /// The tealium utag data mapping settings item identifier.
        /// </value>
        ID TealiumUtagDataMappingSettingsItemID { get; }

        /// <summary>
        /// Gets the tealium settings.
        /// </summary>
        /// <value>
        /// The tealium settings.
        /// </value>
        ITealiumSettings TealiumSettings { get; }

    }
}
