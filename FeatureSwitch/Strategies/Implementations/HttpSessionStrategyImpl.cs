using System;

namespace FeatureSwitch.Strategies.Implementations
{
    public class HttpSessionStrategyImpl : BaseStrategyImpl
    {
        public override bool Read()
        {
            return HttpContextFactory.Current.Session != null && ConvertToBoolean(HttpContextFactory.Current.Session[Context.Key]);
        }

        public override void Write(bool state)
        {
            HttpContextFactory.Current.Session.CheckNull(() => new InvalidOperationException("HttpSession not available"));

            HttpContextFactory.Current.Session[Context.Key] = state;
        }
    }
}
