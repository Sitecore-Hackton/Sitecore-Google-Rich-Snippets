using System.Collections.Generic;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.GoogleStructureData.Models
{
    /// <summary>
    /// Defines a rendering model for multiple navigation items.
    /// </summary>
    public class NavigationItems : RenderingModel
  {
    public IList<NavigationItem> Items { get; set; }
  }
}