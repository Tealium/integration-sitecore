
# Documentation

Please review the pdf documentation in this directory

# Examples

- A "./sample_layout.aspx" file shows changes needed in a layout page to include this Tealium module.

- This file is a modified copy of the "./wwwroot/Sitecore/Website/layouts/Sample layout.aspx" file that is prebuilt with Sitecore

# Advanced Features

- For enabling the Custom UDO feature, an example "MyWebsite.ComputedFields.MyComputedFieldMapper, MyWebsite.ComputedFields" would be implemented as follows

```cs
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
```

- The documentation describes managing multiple web properties.  This requires renaming ./Tealium.Sitecore.TagManagement/App_Config/Include/Tealium.Sitecore.TagManagement.config.example to just "Tealium.Sitecore.TagManagement.config" (this .config file referred to in the documentation.)



