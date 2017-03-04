using Sitecore.Data.Items;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    public class ItemFieldNameDataField : BaseDataField
    {
        public string ItemFieldName { get; set; }

        public override object Execute(Item item)
        {
            if (item == null)
                return null;

            if (item.Fields != null && item.Fields[ItemFieldName] != null)
            {
                return item.Fields[ItemFieldName];
            }

            return null;
        }

        public override bool IsEditable { get { return true; } }
    }
}