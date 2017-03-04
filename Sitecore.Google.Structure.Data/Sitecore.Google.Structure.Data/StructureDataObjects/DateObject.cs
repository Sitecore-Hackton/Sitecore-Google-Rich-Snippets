using System.Web;
using Sitecore.Data.Fields;

namespace Sitecore.Feature.GoogleStructureData.StructureDataObjects
{
    public class DateObject : BaseObject
    {
        public override HtmlString Render(object value)
        {
            var field = value as Field;
            if (field != null)
            {
                value = field.Value;
            }

            return null;
        }
    }
}