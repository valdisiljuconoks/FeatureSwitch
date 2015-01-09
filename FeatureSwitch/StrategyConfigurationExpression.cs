using System;
using FeatureSwitch.Strategies;

namespace FeatureSwitch
{
    public class StrategyConfigurationExpression<TStrategy> where TStrategy : FeatureStrategyAttribute
    {
        private readonly FeatureContext _context;

        public StrategyConfigurationExpression(FeatureContext context)
        {
            _context = context;
        }

        public void Use(Type implementation)
        {
            throw new NotImplementedException();
        }

        public void Use<TImpl>() where TImpl : IStrategy
        {
            var strategyType = typeof(TStrategy);
            if (_context.AdditionalStrategies.Keys.Contains(strategyType))
            {
                // swap already registered strategy
                _context.AdditionalStrategies[strategyType] = typeof(TImpl);
            }
            else
            {
                _context.AdditionalStrategies.Add(strategyType, typeof(TImpl));
            }
        }
    }
}
