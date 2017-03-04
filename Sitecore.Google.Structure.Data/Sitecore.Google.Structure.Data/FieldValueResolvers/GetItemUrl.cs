using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    public class GetItemUrl : BaseFieldValueResolver
    {
        public override string Execute(object value)
        {
            var item = value as Item;
            return item != null ? item.Url() : string.Empty;
        }
    }
}