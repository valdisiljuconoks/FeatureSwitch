using FeatureToggle.Strategies;

namespace FeatureToggle
{
    public class StrategyConfigurationExpression<TStrategy> where TStrategy : FeatureStrategyAttribute
    {
        private readonly FeatureContext context;

        public StrategyConfigurationExpression(FeatureContext context)
        {
            this.context = context;
        }

        public void Use<TImpl>() where TImpl : IStrategy
        {
            var strategyType = typeof(TStrategy);
            if (this.context.AdditionalStrategies.Keys.Contains(strategyType))
            {
                // swap already registered strategy
                this.context.AdditionalStrategies[strategyType] = typeof(TImpl);
            }
            else
            {
                this.context.AdditionalStrategies.Add(strategyType, typeof(TImpl));
            }
        }
    }
}
