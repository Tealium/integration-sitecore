namespace Tealium.Sitecore.TagManagement.Settings
{
	using global::Sitecore.Data.Items;
	using Extensions;

    public class TealiumSettings : ITealiumSettings
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITealiumSettings" /> is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enable; otherwise, <c>false</c>.
        /// </value>
        public bool Enable { get; private set; }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public string Account { get; private set; }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public string Profile { get; private set; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public string Environment { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [enable utag synchronize js].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable utag synchronize js]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableUtagSyncJs { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [enable custom udo].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable custom udo]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCustomUdo { get; private set; }

        /// <summary>
        /// Gets the type of the custom udo.
        /// </summary>
        /// <value>
        /// The type of the custom udo.
        /// </value>
        public string CustomUdoType { get; private set; }

		/// <summary>
		/// Gets the name of the website.
		/// </summary>
		/// <value>
		/// The name of the website.
		/// </value>
	    public string WebsiteName { get; private set; }

	    /// <summary>
        /// Initializes a new instance of the <see cref="TealiumSettings"/> class.
        /// </summary>
        /// <param name="settingsItem">The settings item.</param>
        public TealiumSettings(BaseItem settingsItem)
        {
            InitSettings(settingsItem);
        }

        private void InitSettings(BaseItem settingsItem)
        {
            Enable = settingsItem.Fields[Const.FieldIDs.TealiumSettings.Enable].GetIdValue() == Const.Options.Enabled;
            Account = settingsItem.Fields[Const.FieldIDs.TealiumSettings.Account].Value;
            Profile = settingsItem.Fields[Const.FieldIDs.TealiumSettings.Profile].Value;
            Environment = settingsItem.Fields[Const.FieldIDs.TealiumSettings.Environment].Value;
            EnableUtagSyncJs = settingsItem.Fields[Const.FieldIDs.TealiumSettings.EnableUtagSyncJs].GetBoolValue();
            EnableCustomUdo = settingsItem.Fields[Const.FieldIDs.TealiumSettings.EnableCustomUDO].GetBoolValue();
            CustomUdoType = settingsItem.Fields[Const.FieldIDs.TealiumSettings.CustomUDOType].Value;
            WebsiteName = settingsItem[Const.FieldIDs.TealiumSettings.WebsiteName];
        }
    }
}
