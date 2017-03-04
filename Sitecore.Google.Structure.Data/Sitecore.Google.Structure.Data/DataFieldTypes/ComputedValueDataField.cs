using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.FieldValueResolvers;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    /// <summary>
    /// Class to define a field definition where field value will be received from a Type method.
    /// </summary>
    public class ComputedValueDataField : BaseDataField
    {
        public ComputedValueDataField(Item item) : base(item)
        {
        }

        public string TypeName { get; set; }

        public override object Execute()
        {
            if (Item == null)
                return null;

            if (!string.IsNullOrEmpty(TypeName) && TypeHelper.LooksLikeTypeName(TypeName))
            {
                var resolver = TypeHelper.CreateObject(TypeName) as BaseFieldValueResolver;
                if (resolver != null)
                {
                    return resolver.Execute(Item);
                }
            }

            return null;
        }

        public override bool IsEditable
        {
            get
            {
                return false;
                
            }
        }

        public override HtmlString Render()
        {
            return new HtmlString(Execute().ToString());
        }
    }
}