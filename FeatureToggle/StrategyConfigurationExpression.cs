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

        public void Use<TReader>() where TReader : IStrategyStorageReader
        {
            var strategyType = typeof(TStrategy);
            if (this.context.Readers.Keys.Contains(strategyType))
            {
                // swap already registered strategy
                this.context.Readers[strategyType] = typeof(TReader);
            }
            else
            {
                this.context.Readers.Add(strategyType, typeof(TReader));
            }
        }
    }
}
