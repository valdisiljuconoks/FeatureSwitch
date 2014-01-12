using System;
using System.Web;

namespace FeatureSwitch
{
    public class HttpContextFactory
    {
        private static HttpContextBase ctx;
        public static HttpContextBase Current
        {
            get
            {
                if (ctx != null)
                {
                    return ctx;
                }

                if (HttpContext.Current == null)
                {
                    throw new InvalidOperationException("HttpContext not available");
                }

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public static void SetCurrentContext(HttpContextBase context)
        {
            ctx = context;
        }
    }
}
