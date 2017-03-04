using Sitecore.Data.Items;

namespace Sitecore.Feature.GoogleStructureData.DataFieldTypes
{
    public abstract class BaseDataField
    {
        public abstract object Execute(Item item);

        public abstract bool IsEditable { get; }
    }
}
