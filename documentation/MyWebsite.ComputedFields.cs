/*
"C:\Program Files (x86)\MSBuild\14.0\Bin\csc.exe" /lib:"C:\inetpub\wwwroot\Sitecore1\Website\bin" /reference:"Tealium.Sitecore.TagManagement.dll" /target:library MyWebsite.ComputedFields.cs
*/

using System.Web;
using System.Collections.Generic;
using Tealium.Sitecore.TagManagement.Mappings;

namespace MyWebsite.ComputedFields
{
    public class MyComputedFieldMapper : IComputedFieldMapper
    {
        public void AddComputedFields(IDictionary<string,object> utagParams)
        {
            utagParams.Add("search_term", "testing123");
	}
    }
}

