using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    public class GetItemName : BaseFieldValueResolver
    {
        public override string Execute(object value)
        {
            var item = value as Item;
            return item != null ? item.DisplayName : string.Empty;
        }
    }
}