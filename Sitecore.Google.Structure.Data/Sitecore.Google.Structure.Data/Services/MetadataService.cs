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

        private IDictionary<string, DataFieldWrapper> FillMapping(Rendering rendering)
        {
            var articleMappings = Context.Database.GetItem(Constants.RenderingTypeSitecore.ArticleMappingId);

            if (!articleMappings.HasChildren)
            {
                return _fields;
            }

            foreach (Item mappingChildern in articleMappings.Children)
            {
                if (!mappingChildern.HasChildren)
                {
                    AddField(articleMappings, mappingChildern);
                    return _fields;
                }

                foreach (Item mappingChildernChildern in mappingChildern.Children)
                {

                    MultilistField multilistField = mappingChildernChildern.Fields[Constants.DataFieldItemName.OverrideTemplate];

                    if (multilistField == null)
                    {
                        continue;
                    }
                    if (multilistField.TargetIDs.Contains(rendering.Item.TemplateID))
                    {
                        AddField(articleMappings, mappingChildern);
                    }
                }
            }

            return _fields;
        }


        private void AddField(Item parnetItem, Item item)
        {
            switch (item.TemplateID.ToString())
            {
                case Constants.GstructuredTemplate.ItemFieldNameDataField:
                case Constants.GstructuredTemplate.OverrideItemFieldNameDataField:
                    {
                        var fielditem = new ItemFieldNameDataField(_rendering.Item)
                        {
                            ItemFieldName = item.Fields[Constants.DataFieldItemName.ItemFieldName].Value
                        };

                        _fields.Add(parnetItem.ID.ToString(), new DataFieldWrapper(fielditem));
                        break;
                    }
                case Constants.GstructuredTemplate.ComputedValueDataField:
                case Constants.GstructuredTemplate.OverrideComputedValueDataField:
                    {
                        var fielditem = new ComputedValueDataField(_rendering.Item)
                        {
                            TypeName = item.Fields[Constants.DataFieldItemName.FiledTypeName].Value
                        };

                        _fields.Add(parnetItem.ID.ToString(), new DataFieldWrapper(fielditem));

                        break;
                    }
                case Constants.GstructuredTemplate.ReferenceItemFieldNameDataField:
                case Constants.GstructuredTemplate.OverrideReferenceItemFieldNameDataField:
                    {
                        var fielditem = new ReferenceItemFieldNameDataField(_rendering.Item)
                        {
                            ItemFieldName = item.Fields[Constants.DataFieldItemName.ItemFieldName].Value,
                            ReferenceItemTemplateId = item.Fields[Constants.DataFieldItemName.FiledTypeName].Value,
                            RefernceItemFieldName = item.Fields[Constants.DataFieldItemName.ReferenceItemFieldName].Value
                        };
                        _fields.Add(parnetItem.ID.ToString(), new DataFieldWrapper(fielditem));

                        break;
                    }
            }
        }
    }
}