namespace Tealium.Sitecore.TagManagement.Settings
{
    public interface ITealiumSettings
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITealiumSettings"/> is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enable; otherwise, <c>false</c>.
        /// </value>
        bool Enable { get; }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        string Account { get; }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        string Profile { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        string Environment { get; }

        /// <summary>
        /// Gets a value indicating whether [enable utag synchronize js].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable utag synchronize js]; otherwise, <c>false</c>.
        /// </value>
        bool EnableUtagSyncJs { get; }

        /// <summary>
        /// Gets a value indicating whether [enable custom udo].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable custom udo]; otherwise, <c>false</c>.
        /// </value>
        bool EnableCustomUdo { get; }

        /// <summary>
        /// Gets the type of the custom udo.
        /// </summary>
        /// <value>
        /// The type of the custom udo.
        /// </value>
        string CustomUdoType { get; }

		/// <summary>
		/// Gets the name of the website.
		/// </summary>
		/// <value>
		/// The name of the website.
		/// </value>
		string WebsiteName { get; }
    }
}
