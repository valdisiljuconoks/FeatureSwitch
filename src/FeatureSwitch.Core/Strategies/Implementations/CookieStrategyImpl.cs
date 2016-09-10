using Microsoft.AspNetCore.Http;

namespace FeatureSwitch.Strategies.Implementations
{
    public class CookieStrategyImpl : BaseStrategyImpl
    {
        private readonly IHttpContextAccessor _requestAccessor;
        private string _cookieName;

        public CookieStrategyImpl(IHttpContextAccessor requestAccessor)
        {
            _requestAccessor = requestAccessor;
        }

        public override void Initialize(ConfigurationContext configurationContext)
        {
            base.Initialize(configurationContext);

            _cookieName = Context.Key + "_Cookie";
        }

        public override bool Read()
        {
            return _requestAccessor.HttpContext?.Request.Cookies[_cookieName] == "1";
        }

        public override void Write(bool state)
        {
            _requestAccessor.HttpContext.Response.Cookies.Append(_cookieName, state ? "1" : "0", new CookieOptions { HttpOnly = true });
        }
    }
}
