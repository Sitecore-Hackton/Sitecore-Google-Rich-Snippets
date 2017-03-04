using System.Web;
using Sitecore.Data.Fields;

namespace Sitecore.Feature.GoogleStructureData.StructureDataObjects
{
    /// <summary>
    /// This type defines the structured data value for the date or date time types.
    /// </summary>
    public class DateObject : BaseObject
    {
        public override HtmlString Render(object value)
        {
            var field = value as Field;
            if (field != null)
            {
                value = field.Value;
            }
            
            return new HtmlString(value.ToString());
        }
    }
}