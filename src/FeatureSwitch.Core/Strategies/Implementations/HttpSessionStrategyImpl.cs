using System;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FeatureSwitch.Strategies.Implementations
{
    public class HttpSessionStrategyImpl : BaseStrategyImpl
    {
        private readonly IHttpContextAccessor _requestAccessor;

        public HttpSessionStrategyImpl(IHttpContextAccessor requestAccessor)
        {
            _requestAccessor = requestAccessor;
        }

        public override bool Read()
        {
            if(_requestAccessor.HttpContext.Session == null)
            {
                return false;
            }

            byte[] val;
            // TODO
            return _requestAccessor.HttpContext.Session.TryGetValue(Context.Key, out val) && ConvertToBoolean(val);
        }

        public override void Write(bool state)
        {
            _requestAccessor.HttpContext.Session.CheckNull(() => new InvalidOperationException("HttpSession not available"));


            // TODO
            _requestAccessor.HttpContext.Session.Set(Context.Key, Encoding.UTF8.GetBytes(state.ToString()));
        }
    }
}
