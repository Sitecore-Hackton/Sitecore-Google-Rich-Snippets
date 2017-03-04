using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Data.Fields;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    public class ReferenceItemFieldNameDataField : BaseDataField
    {
        public ReferenceItemFieldNameDataField(Item item) : base(item)
        {
        }

        public string ItemFieldName { get; set; }

        public string ReferenceItemTemplateId { get; set; }

        public string RefernceItemFieldName { get; set; }

        private Item _referenceItem;

        private Field _itemField;

        private Field _referenceItemField;

        public override object Execute()
        {
            if (Item == null)
                return null;

            if (string.IsNullOrEmpty(ItemFieldName)
                || string.IsNullOrEmpty(ReferenceItemTemplateId)
                || string.IsNullOrEmpty(RefernceItemFieldName))
            {
                return null;
            }

            GetField(Item);

            if (_referenceItemField != null)
            {
                return _referenceItemField.Value;
            }

            return null;
        }

        public override bool IsEditable
        {
            get
            {
                GetField(Item);
                return _referenceItem != null &&_referenceItemField != null;
            }
        }

        public override HtmlString Render()
        {
            var fieldId = GetFieldID(Item);

            if (fieldId.IsNull)
                return new HtmlString(string.Empty);

            return _referenceItem.Field(fieldId);
        }

        private ID GetFieldID(Item item)
        {
            GetField(item);
            if (_referenceItemField != null)
            {
                return _referenceItemField.ID;
            }

            return ID.Null;
        }

        private void GetField(Item item)
        {
            if (_referenceItemField == null)
            {
                if (item.Fields != null && !string.IsNullOrEmpty(ItemFieldName) && item.Fields[ItemFieldName] != null)
                {
                    _itemField = item.Fields[ItemFieldName];

                    var value = item.Fields[ItemFieldName].Value;

                    if (!string.IsNullOrEmpty(value))
                    {
                        var referenceItem = Sitecore.Context.Database.GetItem(new ID(value));
                        if (referenceItem != null && !string.IsNullOrEmpty(ReferenceItemTemplateId) && referenceItem.Template.ID.ToString() == ReferenceItemTemplateId)
                        {
                            _referenceItem = referenceItem;

                            if (referenceItem.Fields != null && !string.IsNullOrEmpty(RefernceItemFieldName) && referenceItem.Fields[RefernceItemFieldName] != null)
                            {
                                _referenceItemField = referenceItem.Fields[RefernceItemFieldName];
                            }
                        }
                    }
                }
            }
        }
    }
}