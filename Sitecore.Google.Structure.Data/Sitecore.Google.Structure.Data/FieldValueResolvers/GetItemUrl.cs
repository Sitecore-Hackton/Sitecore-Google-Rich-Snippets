using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Links;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    public class GetItemUrl : BaseFieldValueResolver
    {
        public override string Execute(object value)
        {
            var item = value as Item;
            var options = LinkManager.GetDefaultUrlOptions();
            options.AlwaysIncludeServerUrl = true;
            return item != null ? item.Url(options) : string.Empty;
        }
    }
}