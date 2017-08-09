namespace Tealium.Sitecore.TagManagement
{
	using System.Web;

    public interface ITealiumManager
    {
        /// <summary>
        /// Returns Tealium scripts that will go to the <head></head> section.
        /// </summary>
        /// <returns></returns>
        IHtmlString HeadInjections();

        /// <summary>
        /// Returns Tealium scripts that will go to the <body></body> section.
        /// </summary>
        /// <returns></returns>
        IHtmlString BodyInjections();
    }
}
