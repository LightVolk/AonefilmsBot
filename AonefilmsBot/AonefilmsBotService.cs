using System;

namespace AonefilmsBot
{
    /// <summary>
    /// Служба запуска AonefilmsBot.
    /// </summary>
    class AonefilmsBotService
    {
        private AonefilmsBot bot;

        /// <summary>
        /// Запуск службы.
        /// </summary>
        public void Start()
        {
            try
            {
                Config.Load();

                bot = new AonefilmsBot();
                bot.Start();
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Ошибка запуск бота { exc }");
                throw;
            }
        }
    }
}
