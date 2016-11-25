using System;
using Microsoft.AspNetCore.Http;

namespace FeatureSwitch.Strategies.Implementations
{
    public class QueryStringStrategyImpl : BaseStrategyReaderImpl
    {
        private readonly IHttpContextAccessor _accessor;

        public QueryStringStrategyImpl(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override bool Read()
        {
            var key = Context.Key;

            try
            {
                if(_accessor.HttpContext.Request?.QueryString == null)
                {
                    return false;
                }

                var queryString = _accessor.HttpContext.Request.Query;
                if(!queryString.ContainsKey(key))
                {
                    return false;
                }

                return ConvertToBoolean(queryString[key]);
            }
            catch (Exception)
            {
                // TODO: think of a way to add logging extension point
                return false;
            }
        }
    }
}
