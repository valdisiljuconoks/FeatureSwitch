using System.IO;
using Microsoft.Extensions.Configuration;

namespace FeatureSwitch.Strategies.Implementations
{
    public class AppSettingsStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var appSettingsConfig = builder.Build();

            var value = appSettingsConfig["AppSettings:" + Context.Key];
            return ConvertToBoolean(value);
        }
    }
}
