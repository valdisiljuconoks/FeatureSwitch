using System;
using System.Text;
using FeatureSwitch.Strategies;
using Microsoft.AspNetCore.Http;

namespace FeatureSwitch.Core.Strategies.Implementations
{
    public class HttpSessionStrategyImpl : BaseStrategyImpl
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpSessionStrategyImpl(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override bool Read()
        {
            if(_httpContextAccessor.HttpContext.Session == null)
                return false;

            return _httpContextAccessor.HttpContext.Session.TryGetValue(Context.Key, out var val)
                   && ConvertToBoolean(Encoding.ASCII.GetString(val));
        }

        public override void Write(bool state)
        {
            _httpContextAccessor.HttpContext.Session.CheckNull(() => new InvalidOperationException("HttpSession not available"));
            _httpContextAccessor.HttpContext.Session.Set(Context.Key, Encoding.ASCII.GetBytes(state.ToString()));
        }
    }
}
