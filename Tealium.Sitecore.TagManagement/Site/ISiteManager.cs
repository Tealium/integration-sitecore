namespace Tealium.Sitecore.TagManagement.Site
{
	using System.Collections.Generic;

	public interface ISiteManager
	{
		IEnumerable<string> ConfiguredSites { get; }
		string ContextSiteName { get; }
		string ContextLanguageName { get; }
		IEnumerable<string> SitesToIgnore { get; } 
	}
}
