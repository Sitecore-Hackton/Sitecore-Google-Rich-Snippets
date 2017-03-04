using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.DataFieldTypes;
using Sitecore.Feature.GoogleStructureData.FieldValueResolvers;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.GoogleStructureData.Services
{
    /// <summary>
    /// This class defines the mapping logic for a particular structured data rendering and all its configured field mappings.
    /// The current code is limited to work only for article detail rendering.
    /// The logic trverse all the field definitions those are defined under article type in module settings section.
    /// It checks the whether an override is defined for template or not. if yes it treats that as field definition.
    /// If not found than it checks if an override defined for global field definition, if found it picks that.
    /// If not than it picks the default one. 
    /// </summary>
    public class MetadataService
    {
        private IDictionary<string, DataFieldWrapper> _fields = new Dictionary<string, DataFieldWrapper>();
        private Rendering _rendering;

        public IDictionary<string, DataFieldWrapper> GetFields(Rendering rendering)
        {
            _rendering = rendering;
            if (rendering.RenderingItem.ID.Equals(Constants.RenderingTypeSitecore.ArticleTypeRenderingId))
            {
                return FillMapping(rendering);
            }

            return _fields;
        }

        /// <summary>
        /// Loop through all fields defined under the Article type in sitecore.
        /// </summary>
        /// <param name="rendering"></param>
        /// <returns></returns>

        private IDictionary<string, DataFieldWrapper> FillMapping(Rendering rendering)
        {
            var typeItem = Context.Database.GetItem(Constants.RenderingTypeSitecore.ArticleMappingId);

            if (!typeItem.HasChildren)
            {
                return _fields;
            }

            foreach (Item fieldMetadataItem in typeItem.Children)
            {
                if (!fieldMetadataItem.HasChildren)
                {
                    AddField(fieldMetadataItem, fieldMetadataItem);
                }
                else
                {
                    Item defaultOverride = fieldMetadataItem;
                    bool matchFound = false;
                    foreach (Item fieldDefinitionItem in fieldMetadataItem.Children)
                    {

                        MultilistField multilistField =
                            fieldDefinitionItem.Fields[Constants.DataFieldItemName.OverrideTemplate];

                        if (multilistField == null)
                        {
                            defaultOverride = fieldDefinitionItem;
                            continue;
                        }

                        if (multilistField.TargetIDs.Contains(rendering.Item.TemplateID))
                        {
                            AddField(fieldMetadataItem, fieldDefinitionItem);
                            matchFound = true;
                        }
                    }

                    if (!matchFound)
                    {
                        AddField(fieldMetadataItem, defaultOverride);
                    }
                }
            }

            return _fields;
        }

        /// <summary>
        /// Add specific field type class in the collection based on the field definition template.
        /// </summary>
        /// <param name="fieldMetadataItem"></param>
        /// <param name="fieldDefinitionItem"></param>
        private void AddField(Item fieldMetadataItem, Item fieldDefinitionItem)
        {
            switch (fieldDefinitionItem.TemplateID.ToString())
            {
                case Constants.GstructuredTemplate.ItemFieldNameDataField:
                case Constants.GstructuredTemplate.OverrideItemFieldNameDataField:
                    {
                        var fielditem = new ItemFieldNameDataField(_rendering.Item)
                        {
                            ItemFieldName = fieldDefinitionItem.Fields[Constants.DataFieldItemName.ItemFieldName].Value
                        };

                        _fields.Add(fieldMetadataItem.ID.ToString(), new DataFieldWrapper(fielditem));
                        break;
                    }
                case Constants.GstructuredTemplate.ComputedValueDataField:
                case Constants.GstructuredTemplate.OverrideComputedValueDataField:
                    {
                        var fielditem = new ComputedValueDataField(_rendering.Item)
                        {
                            TypeName = fieldDefinitionItem.Fields[Constants.DataFieldItemName.FiledTypeName].Value
                        };

                        _fields.Add(fieldMetadataItem.ID.ToString(), new DataFieldWrapper(fielditem));

                        break;
                    }
                case Constants.GstructuredTemplate.ReferenceItemFieldNameDataField:
                case Constants.GstructuredTemplate.OverrideReferenceItemFieldNameDataField:
                    {
                        var fielditem = new ReferenceItemFieldNameDataField(_rendering.Item)
                        {
                            ItemFieldName = fieldDefinitionItem.Fields[Constants.DataFieldItemName.ItemFieldName].Value,
                            ReferenceItemTemplateId = fieldDefinitionItem.Fields[Constants.DataFieldItemName.ReferenceItemTemplate].Value,
                            RefernceItemFieldName = fieldDefinitionItem.Fields[Constants.DataFieldItemName.ReferenceItemFieldName].Value
                        };
                        _fields.Add(fieldMetadataItem.ID.ToString(), new DataFieldWrapper(fielditem));

                        break;
                    }
            }
        }
    }
}