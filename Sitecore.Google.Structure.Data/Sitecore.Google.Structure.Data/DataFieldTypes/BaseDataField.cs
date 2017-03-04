using System.Web;
using Sitecore.Data.Items;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    public abstract class BaseDataField
    {
        protected Item Item;

        protected BaseDataField(Item item)
        {
            Item = item;
        }

        public abstract object Execute();

        public abstract bool IsEditable { get; }

        public abstract HtmlString Render();
    }
}
