using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.FieldValueResolvers;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    public class ComputedValueDataField : BaseDataField
    {
        public string TypeName { get; set; }

        public override object Execute(Item item)
        {
            if (item == null)
                return null;

            if (!string.IsNullOrEmpty(TypeName) && TypeHelper.LooksLikeTypeName(TypeName))
            {
                var resolver = TypeHelper.CreateObject(TypeName) as BaseFieldValueResolver;
                if (resolver != null)
                {
                    return resolver.Execute(item);
                }
            }

            return null;
        }

        public override bool IsEditable { get { return false; } }
    }
}