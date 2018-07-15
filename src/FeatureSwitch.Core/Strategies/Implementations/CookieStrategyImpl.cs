using FeatureSwitch.Strategies;
using Microsoft.AspNetCore.Http;

namespace FeatureSwitch.Core.Strategies.Implementations
{
    public class CookieStrategyImpl : BaseStrategyImpl
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _cookieName;

        public CookieStrategyImpl(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Initialize(ConfigurationContext configurationContext)
        {
            base.Initialize(configurationContext);

            _cookieName = Context.Key + "_Cookie";
        }

        public override bool Read()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[_cookieName];

            if (cookie == null)
                return false;

            return cookie == "1";
        }

        public override void Write(bool state)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(_cookieName, state ? "1" : "0", new CookieOptions
                                                                                                     {
                                                                                                         HttpOnly = true
                                                                                                     });
        }
    }
}
