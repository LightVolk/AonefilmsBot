﻿using Newtonsoft.Json;
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

        /// <summary>
        /// Файл со случайным фильмом.
        /// </summary>
        public static string randomFilmFile
        {
            get { return Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\Random.txt"); }
        }

        /// <summary>
        /// Файл с лучшими фильмами.
        /// </summary>
        public static string bestFilmFile
        {
            get { return Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\TheBest.txt"); }
        }

        public static Config Instance { get; private set; }

        public static void Load()
        {
            string configText = File.ReadAllText(botConfigFile);

            Instance = JsonConvert.DeserializeObject<Config>(configText);
        }

        public string Token { get; set; }
    }
}
