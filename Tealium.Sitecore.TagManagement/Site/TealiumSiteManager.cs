namespace Tealium.Sitecore.TagManagement.Site
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class TealiumSiteManager : ISiteManager
	{
		protected static ICollection<string> configuredSites = new List<string>(); 

		public IEnumerable<string> ConfiguredSites
		{
			get
			{
				if (!configuredSites.Any())
				{
					var sites = global::Sitecore.Sites.SiteManager.GetSites().Select(x => x.Name).ToArray();
					var sitesToIgnore = this.SitesToIgnore.ToArray();

					configuredSites = sites.Except(sitesToIgnore).OrderBy(x => x).ToList();
				}

				return configuredSites;
			}
		}

		public string ContextSiteName
		{
			get { return global::Sitecore.Context.Site.Name; }
		}

		public string ContextLanguageName
		{
			get { return global::Sitecore.Context.Language.Name; }
		}

		public IEnumerable<string> SitesToIgnore
		{
			get
			{
				var sitesToIgnoreString = global::Sitecore.Configuration.Settings.GetSetting("Tealium.CustomFields.SitesToIgnore", "admin|login|modules_shell|modules_website|publisher|scheduler|service|shell|system");
				return sitesToIgnoreString.Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries).OrderBy(x => x);
			}
		}
	}
}
