using Sitecore.Data.Items;

namespace Sitecore.Feature.GoogleStructureData.Models
{
    /// <summary>
    /// Defines the details for a navigation item.
    /// </summary>
    public class NavigationItem
    {
        public Item Item { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public NavigationItems Children { get; set; }
        public string Target { get; set; }
        public bool ShowChildren { get; set; }
        public int Position { get; set; }
    }
}