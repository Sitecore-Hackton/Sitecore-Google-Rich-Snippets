using System.Web;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    /// <summary>
    /// Class to define a field definition where field value will be received from a sitecore field that is on current rendering item.
    /// </summary>
    public class ItemFieldNameDataField : BaseDataField
    {
        public ItemFieldNameDataField(Item item) : base(item)
        {
        }

        public string ItemFieldName { get; set; }

        private Field _field;

        public override object Execute()
        {
            if (Item == null)
                return null;

            GetField(Item);
            if (_field != null)
            {
                return _field;
            }

            return null;
        }

        public override bool IsEditable
        {
            get
            {
                GetField(Item);
                return _field != null; 
                
            }
        }

        public override HtmlString Render()
        {
            var fieldId = GetFieldID(Item);
            if (fieldId.IsNull)
                return new HtmlString(string.Empty);

            return Item.Field(fieldId);
        }

        private ID GetFieldID(Item item)
        {
            GetField(item);
            if (_field != null)
            {
                return _field.ID;
            }

            return ID.Null;
        }

        private void GetField(Item item)
        {
            if (_field == null)
            {
                if (item.Fields != null && !string.IsNullOrEmpty(ItemFieldName) && item.Fields[ItemFieldName] != null)
                {
                    _field = item.Fields[ItemFieldName];
                }
            }
        }
    }
}