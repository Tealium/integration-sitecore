using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Tealium.Sitecore.TagManagement.Extensions;

namespace Tealium.Sitecore.TagManagement.Mappings
{
    public class Mapping : IMapping
    {
        /// <summary>
        /// Gets a value indicating whether [add item identifier].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item identifier]; otherwise, <c>false</c>.
        /// </value>
        public bool AddItemId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [add item name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item name]; otherwise, <c>false</c>.
        /// </value>
        public bool AddItemName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [add item language].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item language]; otherwise, <c>false</c>.
        /// </value>
        public bool AddItemLanguage { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [add item path].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add item path]; otherwise, <c>false</c>.
        /// </value>
        public bool AddItemPath { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [add item template name].
        /// </summary>
        /// <value>
        /// <c>true</c> if [add item template name]; otherwise, <c>false</c>.
        /// </value>
        public bool AddItemTemplateName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [add item template identifier].
        /// </summary>
        /// <value>
        /// <c>true</c> if [add item template identifier]; otherwise, <c>false</c>.
        /// </value>
        public bool AddItemTemplateId { get; private set; }

        /// <summary>
        /// Gets the custom parameters.
        /// </summary>
        /// <value>
        /// The custom parameters.
        /// </value>
        public IDictionary<string, object> CustomParameters { get; private set; }

        /// <summary>
        /// Gets the utag template mappings.
        /// </summary>
        /// <value>
        /// The utag template mappings.
        /// </value>
        public IDictionary<ID, IDictionary<string, object>> UtagTemplateMappings { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapping"/> class.
        /// </summary>
        /// <param name="mappingSettingsItem">The mapping settings item.</param>
        public Mapping(Item mappingSettingsItem)
        {
            InitMapping(mappingSettingsItem);
        }

        /// <summary>
        /// Initializes the mapping.
        /// </summary>
        /// <param name="mappingSettingsItem">The mapping settings item.</param>
        private void InitMapping(Item mappingSettingsItem)
        {
            CustomParameters = new Dictionary<string, object>();
            UtagTemplateMappings = new Dictionary<ID, IDictionary<string, object>>();

            AddItemId = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.AddItemId].GetBoolValue();
            AddItemName = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.AddItemName].GetBoolValue();
            AddItemLanguage = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.AddItemLanguage].GetBoolValue();
            AddItemPath = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.AddItemPath].GetBoolValue();
            AddItemTemplateName = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.AddItemTemplateName].GetBoolValue();
            AddItemTemplateId = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.AddItemTemplateId].GetBoolValue();

            CustomParameters = mappingSettingsItem.Fields[Const.FieldIDs.MappingSettings.CustomParameters].GetNameValueCollection();

            foreach (var templateMappingItem in mappingSettingsItem.GetChildren().Where(x => x.TemplateID == Const.TemplateIDs.UtagDataTemplateMapping))
            {
                var templateID = templateMappingItem.Fields[Const.FieldIDs.TemplateMapping.MappingTemplate].GetIdValue();
                if (templateID != ID.Null)
                {
                    UtagTemplateMappings.Add(templateID, templateMappingItem.Fields[Const.FieldIDs.TemplateMapping.CustomParameters].GetNameValueCollection());
                }
            }
        }
    }
}
