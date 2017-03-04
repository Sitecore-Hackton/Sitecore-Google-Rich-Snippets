using System.Web;

namespace Sitecore.Feature.GoogleStructureData.StructureDataObjects
{
    /// <summary>
    /// This class defines the base object bahavior for the various structured data objects.
    /// </summary>
    public abstract class BaseObject
    {
        public abstract HtmlString Render(object value);
    }
}