using System;
using FeatureSwitch;
using FeatureSwitch.Strategies;

namespace ConsoleApp.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new FeatureSetBuilder();
            builder.Build();

            var f = FeatureContext.IsEnabled<MyFeature>();
            Console.WriteLine($"Feature {nameof(MyFeature)} is {(f ? "enabled" : "disabled")}.");
        }
    }

    [AlwaysTrue]
    public class MyFeature : BaseFeature { }
}
