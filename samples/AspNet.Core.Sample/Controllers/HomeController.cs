using AspNet.Core.Sample.Features;
using FeatureSwitch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Core.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly FeatureContext2 _context;

        public HomeController(IHttpContextAccessor accessor, FeatureContext2 context)
        {
            _accessor = accessor;
            _context = context;
        }

        public IActionResult Index()
        {
            var b = _context.IsEnabled<MyFeature>();

            return View();
        }
    }
}
