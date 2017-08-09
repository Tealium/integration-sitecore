namespace Tealium.Sitecore.TagManagement
{
	using System;
	using Data;
	using Mappings;
	using Settings;
	using Site;

	public static class TealiumFactory
	{
		private static readonly object _siteSync = new object();
		private static readonly object _mappingsSync = new object();
		private static readonly object _utagSync = new object();
		private static readonly object _managerSync = new object();
		private static readonly object _settingsSync = new object();
		private static readonly object _computedSync = new object();

		private static ISettingsProvider _settingsProvider;
		private static ISiteManager _siteManager;
		private static ITealiumMappingsProvider _mappingsProvider;
		private static IUtagDataProvider _utagDataProvider;
		private static ITealiumManager _manager;
		private static IComputedFieldMapper _computedFieldMapper;

		public static ISiteManager SiteManager
		{
			get
			{
				if (_siteManager == null)
				{
					lock (_siteSync)
					{
						if (_siteManager == null)
						{
							_siteManager = new TealiumSiteManager();
						}
					}
				}

				return _siteManager;
			}
		}

		/// <summary>
		/// Gets the tealium manager.
		/// </summary>
		/// <returns></returns>
		public static ITealiumManager TealiumManager
		{
			get
			{
				if (_manager == null)
				{
					lock (_managerSync)
					{
						if (_manager == null)
						{
							_manager = new TealiumManager(UtagDataProvider, SettingsProvider);
						}
					}
				}

				return _manager;
			}
		}

		/// <summary>
		/// Gets the mappings provider.
		/// </summary>
		/// <returns></returns>
		public static ITealiumMappingsProvider MappingsProvider
		{
			get
			{
				if (_mappingsProvider == null)
				{
					lock (_mappingsSync)
					{
						if (_mappingsProvider == null)
						{
							var type = Type.GetType(SettingsProvider.TealiumMappingsProvider);
							_mappingsProvider = (ITealiumMappingsProvider)Activator.CreateInstance(type, SettingsProvider);
						}
					}
				}

				return _mappingsProvider;
			}
		}

		/// <summary>
		/// Gets the utag data provider.
		/// </summary>
		/// <returns></returns>
		public static IUtagDataProvider UtagDataProvider
		{
			get
			{
				if (_utagDataProvider == null)
				{
					lock (_utagSync)
					{
						if (_utagDataProvider == null)
						{
							var type = Type.GetType(SettingsProvider.TealiumDataProvider);
							_utagDataProvider = (IUtagDataProvider)Activator.CreateInstance(type, MappingsProvider, SettingsProvider);
						}
					}
				}

				return _utagDataProvider;
			}
		}

		/// <summary>
		/// Gets the settings provider.
		/// </summary>
		/// <returns></returns>
		public static ISettingsProvider SettingsProvider
		{
			get
			{
				if (_settingsProvider == null)
				{
					lock (_settingsSync)
					{
						if (_settingsProvider == null)
						{
							_settingsProvider = new SettingsProvider();
						}
					}
				}

				return _settingsProvider;
			}
		}

		/// <summary>
		/// Gets the settings provider.
		/// </summary>
		/// <returns></returns>
		public static IComputedFieldMapper ComputedFieldMapper
		{
			get
			{
				if (_computedFieldMapper == null)
				{
					lock (_computedSync)
					{
						if (_computedFieldMapper == null)
						{
							var typeQualifiedString = SettingsProvider.TealiumSettings.CustomUdoType;
							if (string.IsNullOrEmpty(typeQualifiedString))
							{
								return null;
							}

							var type = Type.GetType(typeQualifiedString);
							_computedFieldMapper = (IComputedFieldMapper)Activator.CreateInstance(type);
						}
					}
				}

				return _computedFieldMapper;
			}
		}
	}
}
