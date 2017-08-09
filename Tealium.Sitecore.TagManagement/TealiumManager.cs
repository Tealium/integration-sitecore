using System;
using System.Text;
using System.Web;
using Sitecore;
using Sitecore.Diagnostics;
using Tealium.Sitecore.TagManagement.Data;
using Tealium.Sitecore.TagManagement.Settings;

namespace Tealium.Sitecore.TagManagement
{
    public class TealiumManager : ITealiumManager
    {
        private const string BODY_SCRIPT_FORMAT = "<script type=\"text/javascript\"> (function(a,b,c,d){{ a='{0}'; b=document; c='script'; d=b.createElement(c); d.src=a; d.type='text/java'+c; d.async=true; a=b.getElementsByTagName(c)[0]; a.parentNode.insertBefore(d,a); }})(); </script>";
        private const string HEAD_SCRIPT_FORMAT = "<script type='text/javascript' src='{0}'></script>";

        public IUtagDataProvider DataProvider { get; protected set; }
        public ISettingsProvider SettingsProvider { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TealiumManager"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        /// <param name="settingsProvider">The settings provider.</param>
        public TealiumManager(IUtagDataProvider dataProvider, ISettingsProvider settingsProvider)
        {
            DataProvider = dataProvider;
            SettingsProvider = settingsProvider;
        }

        /// <summary>
        /// Returns Tealium scripts that will go to the <head></head> section.
        /// </summary>
        /// <returns></returns>
        public virtual IHtmlString HeadInjections()
        {
            if (!IsEnabled() || !SettingsProvider.TealiumSettings.EnableUtagSyncJs)
            {
                return new HtmlString(string.Empty);
            }

            return new HtmlString(GenerateHeadScriptFormat());
        }

        /// <summary>
        /// Returns Tealium scripts that will go to the <body></body> section.
        /// </summary>
        /// <returns></returns>
        public virtual IHtmlString BodyInjections()
        {
            if (!IsEnabled())
            {
                return new HtmlString(string.Empty);
            }

            var sb = new StringBuilder("");
			
			sb.AppendLine("<script type=\"text/javascript\">");
			sb.AppendLine("var utag_data = {");

            try
            {
                foreach (var utagData in DataProvider.UtagData)
                {
                    sb.AppendLine(string.Format("  {0}: {1},", utagData.Key, utagData.Value));
                }
				// Remove the last comma for proper JSON output
				if (sb.ToString().LastIndexOf(',') > 0)
				{
					sb.Remove(sb.ToString().LastIndexOf(','), 1);
				}
				
            }
            catch (Exception ex)
            {
                Log.Error("[TealliumManager]: " + ex.Message, ex, this);
            }


            sb.AppendLine("};");
            sb.AppendLine("</script>");

            sb.AppendLine(GenerateBodyScript());

            return new HtmlString(sb.ToString());
        }

        //HI CUSTOMIZATION - MADE METHOD VIRTUAL
        protected virtual bool IsEnabled()
        {
            return SettingsProvider.TealiumSettings.Enable && Context.PageMode.IsNormal;
        }

        protected string GenerateBodyScript()
        {
            var settings = SettingsProvider.TealiumSettings;

            return string.Format(BODY_SCRIPT_FORMAT, string.Format(
                SettingsProvider.TealiumUtagJsUriFormat, 
                settings.Account, 
                settings.Profile, 
                settings.Environment));
        }

        protected string GenerateHeadScriptFormat()
        {
            var settings = SettingsProvider.TealiumSettings;

            return string.Format(HEAD_SCRIPT_FORMAT, string.Format(
                SettingsProvider.TealiumUtagSyncJsUriFormat,
                settings.Account,
                settings.Profile,
                settings.Environment));
        }
    }
}
