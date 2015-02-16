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
        public ActionResult Update(string name, string state)
        {
            if (state == "on")
            {
                FeatureContext.Enable(name);
            }
            else
            {
                FeatureContext.Disable(name);
            }

            return RedirectToAction("Index");
        }
    }
}
