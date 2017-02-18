using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AonefilmsBot
{
    using CommandHandler = Action<User, string>;

    /// <summary>
	/// Реализация механизма бота.
	/// </summary>
    class AonefilmsBot
    {
        private TelegramBotClient bot;

        private Dictionary<string, CommandHandler> commandHandlers = new Dictionary<string, CommandHandler>();

        /// <summary>
        /// Username бота.
        /// </summary>
        public string BotUsermame
        {
            get { return this.bot.GetMeAsync().Result.Username; }
        }

        public AonefilmsBot()
        {
            this.bot = new Telegram.Bot.TelegramBotClient(Config.Instance.Token);

            this.bot.OnMessage += BotOnMessageReceived;        
        }

        /// <summary>
        /// Запуск бота.
        /// </summary>
        internal void Start()
        {
            this.bot.StartReceiving();
        }

        // Получение нового собщения.
        private void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            User user = new User
            {
                ChatId = e.Message.Chat.Id,
                UserId = e.Message.From.Id,
                Message = e.Message.Text
            };

            ProcessCommand(user);
        }

        // Обработать команду.
        private void ProcessCommand(User user)
        {
            CommandHandler handler;

            user.Message = user.Message.ToUpperInvariant();

            if(this.commandHandlers.TryGetValue(user.Message, out handler))
                handler(user, user.Message);
        }

        /// <summary>
        /// Добавить новый делегат для команды.
        /// </summary>
        /// <param name="handler">Имя выполяющего метода.</param>
        /// <param name="commandName">Команда.</param>
        public void AddCommandHandler(CommandHandler handler, string commandName)
        {
                this.commandHandlers[commandName] = handler;       
        }

        /// <summary>
        /// Отправить сообщение.
        /// </summary>
        /// <param name="chatId">Id пользователя.</param>
        /// <param name="message">Текст сообщения.</param>
        public async void SendText(long chatId, string message, IReplyMarkup keyboardButton = null, ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = false)
        {
            await this.bot.SendChatActionAsync(chatId, ChatAction.Typing);
            await this.bot.SendTextMessageAsync(chatId, message, replyMarkup: keyboardButton, parseMode: parseMode, disableWebPagePreview: disableWebPagePreview);
        }

        /// <summary>
        /// Отправить изображение.
        /// </summary>
        /// <param name="chatId">Id пользователя.</param>
        /// <param name="photo">Ссылка на изображение.</param>
        public async void SendImage(long chatId, string photoLink, IReplyMarkup inlineButton = null, ParseMode parseMode = ParseMode.Default)
        {       
            await this.bot.SendChatActionAsync(chatId, ChatAction.UploadPhoto);
            await this.bot.SendPhotoAsync(chatId, new FileToSend(photoLink), replyMarkup: inlineButton);
        }
      
    }
}
