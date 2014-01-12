using System.Web.Mvc;

namespace FeatureToggle.AspNet.Mvc5
{
    [AccessAuthorization]
    public class FeatureToggleController : Controller
    {
        public ActionResult Index()
        {
            return View(new FeatureToggleViewModel
                        {
                                Features = FeatureContext.GetFeatures(),
                                RouteName = RouteConfiguration.RouteName
                        });
        }

        [HttpPost]
        public void Update(string name, bool state)
        {
            if (state)
            {
                FeatureContext.Enable(name);
            }
            else
            {
                FeatureContext.Disable(name);
            }
        }
    }
}
