using System.Web.SessionState;
using Glimpse.AspNet;

namespace FeatureSwitch.Glimpse
{
    /// <summary>
    ///     Need to implement IRequiresSessionState interface to get access to HttpSession if needed by some of the
    ///     FeatureSwitch strategy.
    ///     This should be considered as just a  workaround for Glimpse handler.
    /// </summary>
    public class SessionHttpHandler : HttpHandler, IRequiresSessionState
    {
    }
}
