using Newtonsoft.Json;
using System;
using System.IO;

namespace AonefilmsBot
{
    /// <summary>
    /// Конфигурация бота.
    /// </summary>
    class Config
    {
        private static string botConfigFile = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\bot.config");

        public static Config Instance { get; private set; }

        public static void Load()
        {
            string configText = File.ReadAllText(botConfigFile);
            Instance = JsonConvert.DeserializeObject<Config>(configText);
        }

        public string Token { get; set; }
    }
}
