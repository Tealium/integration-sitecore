using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Tealium.Sitecore.TagManagement.Mappings;
using Tealium.Sitecore.TagManagement.Settings;

namespace Tealium.Sitecore.TagManagement.Data
{
    public class UtagDataProvider : IUtagDataProvider
    {
        protected ITealiumMappingsProvider MappingsProvider { get; private set; }
        protected ISettingsProvider SettingsProvider { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UtagDataProvider"/> class.
        /// </summary>
        /// <param name="mappingsProvider">The mappings provider.</param>
        /// <param name="settingsProvider">The settings provider.</param>
        public UtagDataProvider(ITealiumMappingsProvider mappingsProvider, ISettingsProvider settingsProvider)
        {
            MappingsProvider = mappingsProvider;
            SettingsProvider = settingsProvider;
        }

        /// <summary>
        /// Gets the utag data.
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, string> UtagData
        {
            get
            {
                var utagData = new Dictionary<string, string>();

                var tealiumSettings = SettingsProvider.TealiumSettings;

                // Setup 'Custom UDO Type' computed field mapper
                IComputedFieldMapper computedFieldMapper = null;
                try
                {
                    computedFieldMapper = TealiumFactory.ComputedFieldMapper;
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("[UtagDataProvider]: Computed Fields Mapper - Custom UDO Type - threw unhandled exception: {0}", ex.Message), ex, this);
                }

                var mapping = MappingsProvider.Mapping;

                var item = Context.Item;

                AddCommonParameters(item, mapping, utagData);

                AddMappedParameters(item, mapping, utagData);

                if (tealiumSettings.EnableCustomUdo && computedFieldMapper != null)
                {
                    var computedFields = new Dictionary<string, object>();

                    try
                    {
                        computedFieldMapper.AddComputedFields(computedFields);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("[UtagDataProvider]: Computed Fields Mapper [{0}] threw unhandled exception: {1}", computedFieldMapper.GetType().Name, ex.Message), ex, this);
                    }

                    foreach (var utag in computedFields)
                    {
                        AddUtag(utagData, utag.Key, utag.Value);
                    }
                }

                return utagData;
            }
        }

        protected virtual void AddCommonParameters(Item item, IMapping mapping, IDictionary<string, string> utagData)
        {
            if (mapping.AddItemId)
            {
                AddUtag(utagData, "item_id", item.ID.ToString());
            }

            if (mapping.AddItemLanguage)
            {
                AddUtag(utagData, "item_language", item.Language.Name);
            }

            if (mapping.AddItemName)
            {
                AddUtag(utagData, "item_name", item.Name);
            }

            if (mapping.AddItemPath)
            {
                AddUtag(utagData, "item_path", item.Paths.FullPath.ToLower());
            }

            if (mapping.AddItemTemplateId)
            {
                AddUtag(utagData, "item_template_id", item.TemplateID.ToString());
            }

            if (mapping.AddItemTemplateName)
            {
                AddUtag(utagData, "item_template_name", item.TemplateName);
            }

            foreach (var customParam in mapping.CustomParameters)
            {
                if (!utagData.ContainsKey(customParam.Key))
                {
                    AddUtag(utagData, customParam.Key, customParam.Value);
                }
            }
        }

        protected virtual void AddMappedParameters(Item item, IMapping mapping, IDictionary<string, string> utagData)
        {
            foreach (var templateMapping in mapping.UtagTemplateMappings)
            {
                if (InheritedFrom(item, templateMapping.Key))
                {
                    foreach (var fieldMapping in templateMapping.Value)
                    {
                        AddUtag(utagData, fieldMapping.Key, GetFieldValue(item, fieldMapping.Value.ToString()));
                    }
                }
            }
        }

        protected virtual object GetFieldValue(Item item, string name)
        {
            try
            {
                var splittedFieldName = name.Split(new[] {"->"}, StringSplitOptions.RemoveEmptyEntries);

                var fieldName = splittedFieldName.Length == 2 ? splittedFieldName[0] : name;
                var fieldValue = item[fieldName];

                var splittedValue = fieldValue.Split(new[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (splittedFieldName.Length == 2)
                {
                    var targetItems = splittedValue
                        .Select(x => ID.IsID(x) ? item.Database.GetItem(new ID(x)) : null);

                    var values = targetItems
                        .Where(targetItem => targetItem != null)
                        .Select(targetItem => targetItem[splittedFieldName[1]])
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToArray();

                    if (values.Length == 1)
                    {
                        return values[0];
                    }

                    return values;
                }

                if (splittedValue.Length == 1)
                {
                    return splittedValue[0];
                }

                return splittedValue;
            }
            catch (Exception ex)
            {
                Log.Error("[UtagDataProvider]: " + ex.Message, ex, new object());
                return string.Empty;
            }
        }

        protected virtual void AddUtag(IDictionary<string, string> utagData, string paramName, object paramValue)
        {
            var value = paramValue is IEnumerable && !(paramValue is string) && !(paramValue is IEnumerable<char>)
                ? "[" + string.Join(",", ((IEnumerable)paramValue).Cast<object>().Select(x => "\"" + x.ToString() + "\"")) + "]" 
                : "\"" + paramValue + "\"";

            utagData.Add(paramName, value);
        }

        protected bool InheritedFrom(Item item, ID mappedTemplateId)
        {
            Assert.ArgumentNotNull(item, "item");
            Assert.IsNotNull(item.Template, "Item template not found.");

            if (item.TemplateID == mappedTemplateId)
            {
                return true;
            }

            var templates = new Dictionary<ID, ID>();

            return CheckBaseTemplates(templates, item.Template, mappedTemplateId);
        }

        protected static bool CheckBaseTemplates(IDictionary<ID, ID> templates, TemplateItem template, ID mappedTemplateId)
        {
            foreach (var baseTemplateItem in template.BaseTemplates)
            {
                if (!templates.ContainsKey(baseTemplateItem.ID))
                {
                    templates.Add(baseTemplateItem.ID, baseTemplateItem.ID);
                }

                if (baseTemplateItem.ID == mappedTemplateId)
                {
                    return true;
                }

                if (baseTemplateItem.ID != TemplateIDs.StandardTemplate)
                {
                    return CheckBaseTemplates(templates, baseTemplateItem, mappedTemplateId);
                }
            }

            return false;
        }
    }
}
