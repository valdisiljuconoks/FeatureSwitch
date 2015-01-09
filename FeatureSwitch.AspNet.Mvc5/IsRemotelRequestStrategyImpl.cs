using System.Web;
using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.AspNet.Mvc
{
    public class IsRemotelRequestStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return HttpContext.Current != null && !HttpContext.Current.Request.IsLocal;
        }
    }
}
