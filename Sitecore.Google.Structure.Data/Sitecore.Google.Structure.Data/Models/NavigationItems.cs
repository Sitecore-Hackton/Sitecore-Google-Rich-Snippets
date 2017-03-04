using System.Collections.Generic;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.GoogleStructureData.Models
{
    public class NavigationItems : RenderingModel
  {
    public IList<NavigationItem> Items { get; set; }
  }
}