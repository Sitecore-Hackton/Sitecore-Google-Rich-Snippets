using System.Web.Mvc;
using Sitecore.Feature.GoogleStructureData.Repositories;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.GoogleStructureData.Controllers
{
    /// <summary>
    /// Controller class.
    /// </summary>
    public class GoogleStructureDataController : Controller
  {
    private readonly INavigationRepository _navigationRepository;

    public GoogleStructureDataController() : this(new NavigationRepository(RenderingContext.Current.ContextItem))
    {
    }

    public GoogleStructureDataController(INavigationRepository navigationRepository)
    {
      this._navigationRepository = navigationRepository;
    }

    public ActionResult Breadcrumb()
    {
      var items = this._navigationRepository.GetBreadcrumb();
      return this.View("Breadcrumb", items);
    }
  }
}