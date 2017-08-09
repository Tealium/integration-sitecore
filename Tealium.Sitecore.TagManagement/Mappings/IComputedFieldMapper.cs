using System.Collections.Generic;

namespace Tealium.Sitecore.TagManagement.Mappings
{
    public interface IComputedFieldMapper
    {
        /// <summary>
        /// Adds the computed fields.
        /// </summary>
        /// <param name="utagParams">The utag parameters.</param>
        void AddComputedFields(IDictionary<string, object> utagParams);
    }
}
