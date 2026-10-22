using System;
using System.Collections.Generic;
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
        private const string BODY_SCRIPT_FORMAT = "<script type=\"text/javascript\"> (function(a,b,c,d){{ a=\"{0}\"; b=document; c='script'; d=b.createElement(c); d.src=a; d.type='text/java'+c; d.async=true; a=b.getElementsByTagName(c)[0]; a.parentNode.insertBefore(d,a); }})(); </script>";
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
            try
            {
                if (!IsEnabled() || !SettingsProvider.TealiumSettings.EnableUtagSyncJs)
                {
                    return new HtmlString(string.Empty);
                }

                return new HtmlString(GenerateHeadScriptFormat());
            }
            catch (Exception ex)
            {
                // tag management must never break page rendering
                Log.Error("[TealiumManager]: HeadInjections failed: " + ex.Message, ex, this);
                return new HtmlString(string.Empty);
            }
        }

        /// <summary>
        /// Returns Tealium scripts that will go to the <body></body> section.
        /// </summary>
        /// <returns></returns>
        public virtual IHtmlString BodyInjections()
        {
            try
            {
                if (!IsEnabled())
                {
                    return new HtmlString(string.Empty);
                }

                var sb = new StringBuilder();
                sb.AppendLine("<script type=\"text/javascript\">");
                sb.AppendLine("var utag_data = " + GenerateUtagData() + ";");
                sb.AppendLine("</script>");
                sb.AppendLine(GenerateBodyScript());

                return new HtmlString(sb.ToString());
            }
            catch (Exception ex)
            {
                // tag management must never break page rendering
                Log.Error("[TealiumManager]: BodyInjections failed: " + ex.Message, ex, this);
                return new HtmlString(string.Empty);
            }
        }

        /// <summary>
        /// Builds the utag_data object literal. Keys are encoded here; values come
        /// from the data provider as JavaScript literals (see <see cref="UtagValueEncoder"/>).
        /// </summary>
        protected virtual string GenerateUtagData()
        {
            var entries = new List<string>();

            try
            {
                foreach (var utagData in DataProvider.UtagData)
                {
                    if (string.IsNullOrEmpty(utagData.Key))
                    {
                        continue;
                    }

                    entries.Add("  " + UtagValueEncoder.EncodeString(utagData.Key) + ": " + UtagValueEncoder.ProtectLiteral(utagData.Value));
                }
            }
            catch (Exception ex)
            {
                Log.Error("[TealiumManager]: " + ex.Message, ex, this);
            }

            return entries.Count == 0
                ? "{}"
                : "{" + Environment.NewLine + string.Join("," + Environment.NewLine, entries) + Environment.NewLine + "}";
        }

        //HI CUSTOMIZATION - MADE METHOD VIRTUAL
        protected virtual bool IsEnabled()
        {
            return Context.PageMode.IsNormal && SettingsProvider.TealiumSettings.Enable;
        }

        protected string GenerateBodyScript()
        {
            var settings = SettingsProvider.TealiumSettings;

            return string.Format(BODY_SCRIPT_FORMAT, EncodeUrlForScript(string.Format(
                SettingsProvider.TealiumUtagJsUriFormat,
                EncodePathSegment(settings.Account),
                EncodePathSegment(settings.Profile),
                EncodePathSegment(settings.Environment))));
        }

        protected string GenerateHeadScriptFormat()
        {
            var settings = SettingsProvider.TealiumSettings;

            return string.Format(HEAD_SCRIPT_FORMAT, HttpUtility.HtmlAttributeEncode(string.Format(
                SettingsProvider.TealiumUtagSyncJsUriFormat,
                EncodePathSegment(settings.Account),
                EncodePathSegment(settings.Profile),
                EncodePathSegment(settings.Environment))));
        }

        private static string EncodePathSegment(string value)
        {
            return Uri.EscapeDataString((value ?? string.Empty).Trim());
        }

        /// <summary>
        /// The utag.js url as the inside of a single-quoted JavaScript string
        /// </summary>
        private static string EncodeUrlForScript(string url)
        {
            var literal = UtagValueEncoder.EncodeString(url);
            return literal.Substring(1, literal.Length - 2);
        }
    }
}
