using System.Web;
using System.Web.Routing;
using FeatureSwitch.AspNet.Mvc;

namespace FeatureSwitch.Glimpse
{
    public class AssemblyResourceJavascriptHandler : IHttpHandler
    {
        private readonly RequestContext _requestContext;

        public AssemblyResourceJavascriptHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            var provider = new AssemblyEmbeddedResourceProvider();
            var file = provider.GetFile(context.Request.Path + (context.Request.Path.EndsWith(".js") ? "" : ".js"));

            context.Response.ContentType = "text/javascript";
            using (var fs = file.Open())
            {
                fs.CopyTo(context.Response.OutputStream);
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
