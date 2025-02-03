using Microsoft.Extensions.Configuration;

namespace TrippleZero.Common
{

    public static class EnvironmentManager
    {
        private static readonly Lazy<Dictionary<string, string>> _configurationDictionary;

        static EnvironmentManager()
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(Environment.GetCommandLineArgs())
                    .Build();

            _configurationDictionary = new Lazy<Dictionary<string, string>>(() =>
            {

                var environment = configuration["Environment"];
                if (string.IsNullOrEmpty(environment))
                {
                    throw new ArgumentNullException("The Environment key is not found in the configuration");
                }

                var section = configuration.GetSection(environment);
                if (section == null)
                {
                    throw new ArgumentNullException($"The configuration section for the environment '{environment}' is not found");
                }

                var keyPair = section.AsEnumerable()
                                      .Where(kv => !string.IsNullOrEmpty(kv.Value))
                                      .ToDictionary(kv => kv.Key.Replace($"{environment}:", "",
                                      StringComparison.OrdinalIgnoreCase), kv => kv.Value ??
                                      string.Empty, StringComparer.OrdinalIgnoreCase);
                keyPair.Add("Environment", environment);
                return keyPair;
            });
        }

        public static string GetOrThrow(string key)
        {
            if (!_configurationDictionary.Value.TryGetValue(key, out var value))
            {
                var message = $"The key '{key}' is not found in the configuration for the environment '{_configurationDictionary.Value["Environment"]}'";
                throw new ArgumentNullException(message);
            }

            return value;
        }
    }
}