using System.Web.Mvc;

namespace FeatureSwitch.AspNet.Mvc
{
    [AccessAuthorization]
    public class FeatureSwitchController : Controller
    {
        public ActionResult Index()
        {
            return View(new FeatureSwitchViewModel
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
