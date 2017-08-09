using Sitecore;
using Sitecore.Configuration;

namespace Tealium.Sitecore.TagManagement
{
	using global::Sitecore.Data;

    public static class Const
    {
        public static class FieldIDs
        {
            public static class TealiumSettings
            {
                public static ID Enable = new ID("{D41118E0-A350-41E9-A406-CCD9C81A7AC6}");
                public static ID Account = new ID("{67D4BCCB-278B-444A-BDEA-1AD72A255EE5}");
                public static ID Profile = new ID("{F5DDF8E4-28F8-42BD-BBD3-3A5470B32EDF}");
                public static ID Environment = new ID("{DD2FCDD4-55F2-4CE2-A354-2885DCE69DC9}");
                public static ID EnableUtagSyncJs = new ID("{1A6E955A-A47E-4036-89FD-28C8FC01DE1A}");
                public static ID EnableCustomUDO = new ID("{0C43454F-D20A-47A8-8F87-6DB9B53628DE}");
                public static ID CustomUDOType = new ID("{A6EF4BE0-6DBA-4DE0-AEF8-D80E61E3420D}");
				public static ID WebsiteName = new ID("{DE14CE94-1280-4DF3-9EA3-8AF161A7FE9B}");
            }

            public static class MappingSettings
            {
                public static ID AddItemId = new ID("{7B3D71F2-6B32-468E-8555-84EC708B984B}");
                public static ID AddItemName = new ID("{1E52468B-AF71-45E5-B635-50172D506A78}");
                public static ID AddItemLanguage = new ID("{68243ACC-C4D5-43E2-A308-C13CB5851E7D}");
                public static ID AddItemPath = new ID("{2210A9A3-C554-470E-BBB6-931AA80D7684}");
                public static ID AddItemTemplateName = new ID("{1D13C943-DE19-4DAE-B765-CB75BB807182}");
                public static ID AddItemTemplateId = new ID("{273D42C4-EB1A-46E9-863B-5297AD65C39B}");
                public static ID CustomParameters = new ID("{037C6059-3331-4D9E-832C-D8756D586833}");
				public static ID WebsiteName = new ID("{8773A947-898C-44E4-AB9A-15158B53E790}");
            }

            public static class TemplateMapping
            {
                public static ID MappingTemplate = new ID("{5F00AC7B-0CC8-43DC-B7A2-BAD844A50909}");
                public static ID CustomParameters = new ID("{321B5DEB-F856-4F3F-813B-4447AA0012C2}");
            }
        }

        public static class Options
        {
            public static ID Enabled = new ID("{0D4FD7AD-E535-45BD-8AA8-36198E2F015D}");
            public static ID Disabled = new ID("{8EBC5DCC-F14E-4A53-AB87-53FE1CA1E87B}");
        }

	    public static class TemplateIDs 
	    {
			public static TemplateID UtagDataMappingSettings = new TemplateID(new ID("{5140BFD4-998E-4071-A7CC-A462C0F9061D}"));
			public static TemplateID UtagDataSiteMappingSettings = new TemplateID(new ID("{14566AC3-A103-48F3-A823-A11971DCC26E}"));
			public static TemplateID UtagDataTemplateMapping = new TemplateID(new ID("{A1ECFDBF-E2FA-4D48-87E6-6A5F0A6E895D}"));
	    }
    }
}
