using System.Collections.Generic;

namespace Tealium.Sitecore.TagManagement.Data
{
    public interface IUtagDataProvider
    {
        /// <summary>
        /// Gets the utag data.
        /// </summary>
        /// <value>
        /// The utag data.
        /// </value>
        IDictionary<string, string> UtagData { get; }
    }
}
