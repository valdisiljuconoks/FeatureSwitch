using System;
using System.Linq;
using System.Web.Security;
using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.AspNet.Mvc
{
    public class AuthorizedForStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            var availableForRoles = Context.Key.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
            return availableForRoles.Select(Roles.IsUserInRole).Any(v => v);
        }
    }
}
