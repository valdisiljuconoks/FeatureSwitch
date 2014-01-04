using System;
using System.Linq;

namespace FeatureToggle.Strategies.Implementations
{
    public class QueryStringImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            var key = Context.Key;

            if (HttpContextFactory.Current.Request == null)
            {
                return false;
            }

            if (HttpContextFactory.Current.Request.QueryString == null)
            {
                return false;
            }

            var queryString = HttpContextFactory.Current.Request.QueryString;
            if (!queryString.AllKeys.Contains(key, StringComparer.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return ConvertToBoolean(queryString[key]);
        }
    }
}
