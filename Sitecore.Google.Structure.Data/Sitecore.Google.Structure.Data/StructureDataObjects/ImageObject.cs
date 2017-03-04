using System.Web;
using Sitecore.Data.Fields;

namespace Sitecore.Feature.GoogleStructureData.StructureDataObjects
{
    public class ImageObject : BaseObject
    {
        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
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