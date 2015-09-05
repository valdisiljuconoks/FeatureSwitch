using System;
using System.Web;

namespace FeatureSwitch
{
    public class HttpContextFactory
    {
        private static HttpContextBase _ctx;

        public static HttpContextBase Current
        {
            get
            {
                if (_ctx != null)
                {
                    return _ctx;
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
            _ctx = context;
        }
    }
}
