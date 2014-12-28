using System.Collections;
using FeatureSwitch.Strategies;

namespace FeatureSwitch.Tests.Strategies
{
    public class WritableHashMapStrategyAttribute : FeatureStrategyAttribute
    {
    }

    public class WritableHashMapStrategy2Attribute : FeatureStrategyAttribute
    {
    }

    public class WritableHashtableStrategy : BaseStrategyImpl
    {
        private readonly Hashtable _state = new Hashtable();

        public override bool Read()
        {
            var key = Context.Key;
            return _state.ContainsKey(key) && ConvertToBoolean(_state[key]);
        }

        public override void Write(bool state)
        {
            var key = Context.Key;

            if (_state.ContainsKey(key))
            {
                _state[key] = state;
            }

            else
            {
                _state.Add(key, state);
            }
        }
    }

    public class WritableHashtable2Strategy : WritableHashtableStrategy
    {
    }
}
