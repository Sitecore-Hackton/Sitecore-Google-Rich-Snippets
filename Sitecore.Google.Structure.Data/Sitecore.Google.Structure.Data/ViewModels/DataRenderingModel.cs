using System.Collections.Generic;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.FieldValueResolvers;
using Sitecore.Feature.GoogleStructureData.Models;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.GoogleStructureData.ViewModels
{
    public class DataRenderingModel : RenderingModel
    {
        private IDictionary<string, FieldInfo> _fields = new Dictionary<string, FieldInfo>();

        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);
            var title = new DataField()
            {
                FieldName = "NewsTitle",
                TypeName = "Sitecore.Feature.GoogleStructureData.FieldValueResolvers.GetItemName,Sitecore.Feature.GoogleStructureData"
            };

            _fields.Add(Constants.Items.ArticleFieldItems.HeadlineFieldID.ToString(), new FieldInfo(title));

            var mainentryofpage = new DataField()
            {
                FieldName = "",
                TypeName = "Sitecore.Feature.GoogleStructureData.FieldValueResolvers.GetItemUrl,Sitecore.Feature.GoogleStructureData"
            };

            _fields.Add(Constants.Items.ArticleFieldItems.MainEntryofPageFieldID.ToString(), new FieldInfo(mainentryofpage));
        }

        public bool HasValueFor(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                return !string.IsNullOrEmpty(_fields[fieldId.ToString()].GetFieldValue(this.Rendering.Item));
            }

            return false;
        }

        private bool FieldExists(ID fieldId)
        {
            return _fields.ContainsKey(fieldId.ToString());
        }

        public string GetValue(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                return _fields[fieldId.ToString()].GetFieldValue(this.Rendering.Item);
            }

            return string.Empty;
        }

        public HtmlString Field(ID fieldId)
        {
            var mappedFieldId =_fields[fieldId.ToString()].GetFieldId(this.Rendering.Item);
            if (mappedFieldId.IsNull)
                return null;
            return this.Rendering.Item.Field(mappedFieldId);
        }
    }

    public class FieldInfo
    {
        private string _fieldName;
        private ID _fieldId;
        private string _fieldValue;

        private DataField _dataField;

        public FieldInfo(DataField dataField)
        {
            _dataField = dataField;
        }

        public ID GetFieldId(Item item)
        {
            if (!_fieldId.IsNull)
            {
                return _fieldId;
            }

            if (!string.IsNullOrEmpty(_dataField.FieldName) && item.Fields[_dataField.FieldName] != null)
            {
                _fieldName = _dataField.FieldName;
            }
            else if (!string.IsNullOrEmpty(_dataField.FieldId) && item.Fields[new ID(_dataField.FieldId)] != null)
            {
                _fieldName = item.Fields[new ID(_dataField.FieldId)].Name;
            }

            if (!string.IsNullOrEmpty(_fieldName))
            {
                _fieldId = item.Fields[_fieldName].ID;
            }

            return _fieldId;
        }

        public string GetFieldValue(Item item)
        {
            if (!string.IsNullOrEmpty(_fieldValue))
            {
                return _fieldValue;
            }

            if (!string.IsNullOrEmpty(_dataField.TypeName) && TypeHelper.LooksLikeTypeName(_dataField.TypeName))
            {
                var resolver = TypeHelper.CreateObject(_dataField.TypeName) as BaseFieldValueResolver;
                if (resolver != null)
                {
                    _fieldValue = resolver.Execute(item);
                }
            }

            return _fieldValue;
        }
    }
}