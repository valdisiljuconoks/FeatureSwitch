using System;
using FeatureSwitch.Strategies.Implementations;
using Microsoft.AspNetCore.Http;

namespace FeatureSwitch.Core.Strategies.Implementations
{
    public class QueryStringStrategyImpl : BaseStrategyReaderImpl
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryStringStrategyImpl(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override bool Read()
        {
            var key = Context.Key;

            try
            {
                if(_httpContextAccessor.HttpContext.Request?.QueryString == null)
                    return false;

                var queryString = _httpContextAccessor.HttpContext.Request.Query;
                return queryString.Keys.Contains(key) && ConvertToBoolean(queryString[key]);
            }
            catch (Exception)
            {
                // TODO: think of a way to add logging extension point
                return false;
            }
        }
    }
}
