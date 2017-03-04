using System.Web;

namespace Sitecore.Feature.GoogleStructureData.StructureDataObjects
{
    public abstract class BaseObject
    {
        public abstract HtmlString Render(object value);
    }
}