using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.Models;

namespace Sitecore.Feature.GoogleStructureData.Repositories
{
    /// <summary>
    /// Repository to perform navigation related operations.
    /// </summary>
    public interface INavigationRepository
    {
        Item GetNavigationRoot(Item contextItem);
        NavigationItems GetBreadcrumb();
    }
}