using System.Web;

namespace FeatureSwitch.Strategies.Implementations
{
    public class CookieStrategyImpl : BaseStrategyImpl
    {
        private string _cookieName;

        public override void Initialize(ConfigurationContext configurationContext)
        {
            base.Initialize(configurationContext);

            _cookieName = Context.Key + "_Cookie";
        }

        public override bool Read()
        {
            if (HttpContext.Current == null)
            {
                return false;
            }

            var cookie = HttpContext.Current.Request.Cookies[_cookieName];

            if (cookie == null)
            {
                return false;
            }

            var value = cookie.Value;

            return value == "1";
        }

        public override void Write(bool state)
        {
            var cookie = new HttpCookie(_cookieName, "1") { HttpOnly = true };

            if (HttpContext.Current == null)
            {
                return;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
