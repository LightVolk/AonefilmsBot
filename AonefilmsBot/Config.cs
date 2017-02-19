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
        public static string botConfigPath = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\");

        private static string botConfigName = Environment.ExpandEnvironmentVariables("bot.config");

        public static Config Instance { get; private set; }

        public static void Load()
        {
            string botConfigFile = Path.Combine(botConfigPath, botConfigName);

            string configText = File.ReadAllText(botConfigFile);

            Instance = JsonConvert.DeserializeObject<Config>(configText);
        }

        public string Token { get; set; }

        /// <summary>
        /// Файл с лучшими фильмами.
        /// </summary>
        public static string bestFilmFile
        {
            get { return Path.Combine(botConfigPath, "TheBest.txt"); }
        }

        /// <summary>
        /// Файл со случайным фильмом.
        /// </summary>
        public static string randomFilmFile
        {
            get { return Path.Combine(botConfigPath, "Random.txt"); }
        }
    }
}
