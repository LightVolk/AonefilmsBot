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
        private static string ConfigPath = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\bot.config");


        public static string botConfigPath = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\");

        public static string bestFilmFile = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\TheBest.txt");

        public static string randomFilmFile = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\Random.txt");

        public static string DanoFile = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\Dano.txt");


        public static Config Instance { get; private set; }

        public static void Load()
        {
            string configText = File.ReadAllText(ConfigPath);

            Instance = JsonConvert.DeserializeObject<Config>(configText);
        }


        public string Token { get; set; }
    }
}
