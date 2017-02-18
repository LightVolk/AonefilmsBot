using System;

namespace AonefilmsBot
{
    /// <summary>
	/// Реализация механизма бота.
	/// </summary>
    class AonefilmsBot
    {
        private Telegram.Bot.TelegramBotClient bot;

        public string UserName
        {
            get { return this.bot.GetMeAsync().Result.Username; }
        }

        public AonefilmsBot()
        {
            this.bot = new Telegram.Bot.TelegramBotClient(Config.Instance.Token);

            //this.bot.OnMessage += Bot_OnMessage;
        }

        /// <summary>
        /// Запуск бота.
        /// </summary>
        internal void Start()
        {
            this.bot.StartReceiving();
        }


    }
}
