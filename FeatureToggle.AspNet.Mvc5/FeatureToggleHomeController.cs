using System.Web.Mvc;

namespace FeatureToggle.AspNet.Mvc5
{
    public class FeatureToggleHomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new FeatureToggleHomeViewModel
            {
                Features = FeatureContext.GetFeatures()
            });
        }
    }
}
