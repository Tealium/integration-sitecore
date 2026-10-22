namespace Tealium.Sitecore.TagManagement.CustomFields
{
	using System.Web;
	using System.Web.UI;
	using System.Linq;

	public class SiteSelectorDroplist : global::Sitecore.Web.UI.HtmlControls.Control
	{
		public SiteSelectorDroplist()
		{
			this.Class = "SitecoreContentControl";
			this.Activation = true;
		}

		public bool HasPostData { get; set; }
		public string Source { get; set; }

		protected override void DoRender(HtmlTextWriter output)
		{
			string err = null;
			output.Write("<select style='width:100%;'" + this.GetControlAttributes() + ">");
			output.Write("<option value=\"\"></option>");

			var configuredSites = TealiumFactory.SiteManager.ConfiguredSites.ToArray();

			if (!configuredSites.Any())
			{
				err = "No sites are configured to display. Please check your configuration. Use 'Tealium.CustomFields.SitesToIgnore' to ignore required sites and use <site> nodes to configure sitecore websites.";
			}
			else
			{

				bool valueFound = string.IsNullOrEmpty(this.Value);

				foreach (var site in configuredSites)
				{
					valueFound = valueFound || site == this.Value;
					var encodedSite = HttpUtility.HtmlEncode(site);
					output.Write(@"<option value=""{0}"" {1}>{2}</option>", encodedSite, this.Value == site ? " selected=\"selected\"" : string.Empty, encodedSite);
				}

				if (!valueFound)
				{
					err = "Value not in the selection list.";
				}
			}

			if (err != null)
			{
				var encodedValue = HttpUtility.HtmlEncode(this.Value);
				output.Write("<optgroup label=\"" + HttpUtility.HtmlEncode(err) + "\">");
				output.Write("<option value=\"" + encodedValue + "\" selected=\"selected\">" + encodedValue + "</option>");
				output.Write("</optgroup>");
			}

			output.Write("</select>");

			if (err != null)
			{
				output.Write("<div style=\"color:#999999;padding:2px 0px 0px 0px\">{0}</div>", HttpUtility.HtmlEncode(err));
			}
		}

		protected override bool LoadPostData(string value)
		{
			this.HasPostData = true;

			if (value == null)
			{
				return false;
			}

			if (this.GetViewStateString("Value") != value)
			{
				SetModified();
			}

			this.SetViewStateString("Value", value);
			return true;
		}

		private static void SetModified()
		{
			global::Sitecore.Context.ClientPage.Modified = true;
		}
	}
}
