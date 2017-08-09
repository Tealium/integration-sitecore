using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Fields;

namespace Tealium.Sitecore.TagManagement.Extensions
{
    public static class FieldExtensions
    {
        public static bool GetBoolValue(this Field field)
        {
            return field.Value == "1";
        }

        public static IDictionary<string, object> GetNameValueCollection(this Field field)
        {
            var collection = new Dictionary<string, object>(); 

            var namevaluePairsString = HttpUtility.UrlDecode(field.Value).Split(new[] {"&"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pairString in namevaluePairsString)
            {
                var pair = pairString.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2 && !collection.ContainsKey(pair[0]))
                {
                    if (pair[1].Split(new[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                    {
                        collection.Add(pair[0], pair[1].Split(new[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                    }
                    else
                    {
                        collection.Add(pair[0], pair[1]);
                    }
                }
            }

            return collection;
        }

        public static ID GetIdValue(this Field field)
        {
            if (ID.IsID(field.Value))
            {
                return new ID(field.Value);
            }

            return ID.Null;
        }
    }
}
