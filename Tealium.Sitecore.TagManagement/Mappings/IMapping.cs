using System.Collections.Generic;
using Sitecore.Data;

namespace Tealium.Sitecore.TagManagement.Mappings
{
    public interface IMapping
    {
        /// <summary>
        /// Gets a value indicating whether [add item identifier].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item identifier]; otherwise, <c>false</c>.
        /// </value>
        bool AddItemId { get; }

        /// <summary>
        /// Gets a value indicating whether [add item name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item name]; otherwise, <c>false</c>.
        /// </value>
        bool AddItemName { get; }

        /// <summary>
        /// Gets a value indicating whether [add item language].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item language]; otherwise, <c>false</c>.
        /// </value>
        bool AddItemLanguage { get; }

        /// <summary>
        /// Gets a value indicating whether [add item path].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item path]; otherwise, <c>false</c>.
        /// </value>
        bool AddItemPath { get; }

        /// <summary>
        /// Gets a value indicating whether [add item template name].
        /// </summary>
        /// <value>
        /// <c>true</c> if [add item template name]; otherwise, <c>false</c>.
        /// </value>
        bool AddItemTemplateName { get; }

        /// <summary>
        /// Gets a value indicating whether [add item template identifier].
        /// </summary>
        /// <value>
        /// <c>true</c> if [add item template identifier]; otherwise, <c>false</c>.
        /// </value>
        bool AddItemTemplateId { get; }

        /// <summary>
        /// Gets the custom parameters.
        /// </summary>
        /// <value>
        /// The custom parameters.
        /// </value>
        IDictionary<string, object> CustomParameters { get; }

        /// <summary>
        /// Gets the utag template mappings.
        /// </summary>
        /// <value>
        /// The utag template mappings.
        /// </value>
        IDictionary<ID, IDictionary<string, object>> UtagTemplateMappings { get; }
    }
}