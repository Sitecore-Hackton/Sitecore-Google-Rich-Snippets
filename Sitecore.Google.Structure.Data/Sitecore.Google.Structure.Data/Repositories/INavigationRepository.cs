using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.Models;

namespace Sitecore.Feature.GoogleStructureData.Repositories
{
    public interface INavigationRepository
    {
        Item GetNavigationRoot(Item contextItem);
        NavigationItems GetBreadcrumb();
    }
}