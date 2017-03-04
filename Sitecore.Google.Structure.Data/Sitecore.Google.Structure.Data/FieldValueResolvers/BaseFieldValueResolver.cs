using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    public abstract class BaseFieldValueResolver
    {
        public abstract string Execute(object value);
    }
}