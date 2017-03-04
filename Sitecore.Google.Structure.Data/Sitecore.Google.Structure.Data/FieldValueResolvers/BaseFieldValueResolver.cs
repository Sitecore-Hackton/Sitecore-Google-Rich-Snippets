using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    /// <summary>
    /// Base class for field value resolver.
    /// This is the base class for creating custom types that can be defined in field mappings.
    /// </summary>
    public abstract class BaseFieldValueResolver
    {
        public abstract string Execute(object value);
    }
}