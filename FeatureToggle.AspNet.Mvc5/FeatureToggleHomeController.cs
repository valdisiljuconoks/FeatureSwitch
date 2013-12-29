using System;
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
