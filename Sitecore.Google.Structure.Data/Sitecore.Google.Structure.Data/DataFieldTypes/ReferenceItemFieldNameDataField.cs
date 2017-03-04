using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    public class ReferenceItemFieldNameDataField : BaseDataField
    {
        public string ItemFieldName { get; set; }

        public string ReferenceItemTemplateId { get; set; }

        public string RefernceItemFieldName { get; set; }

        public override object Execute(Item item)
        {
            if (item == null)
                return null;

            if (item.Fields != null && item.Fields[ItemFieldName] != null)
            {
                var value = item.Fields[ItemFieldName].Value;

                if (!string.IsNullOrEmpty(value))
                {
                    var referenceItem = Sitecore.Context.Database.GetItem(new ID(value));
                    if (referenceItem != null)
                    {
                        if (referenceItem.Fields != null && referenceItem.Fields[RefernceItemFieldName] != null)
                        {
                            return referenceItem.Fields[RefernceItemFieldName].Value;
                        }
                    }
                }
            }

            return null;
        }

        public override bool IsEditable { get { return false; } }
    }
}