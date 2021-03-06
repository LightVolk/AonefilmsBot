﻿using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;

namespace AonefilmsBot
{ 
    using CommandHandler = Action<User, string>;

    using CounterHandler = Action<User, string, Int16>;

    /// <summary>
	/// Реализация механизма бота.
	/// </summary>
    class AonefilmsBot
    {
        private Telegram.Bot.TelegramBotClient bot;

        private Dictionary<string, CommandHandler> commandHandlers = new Dictionary<string, CommandHandler>();

        private Dictionary<string, CounterHandler> counterHandlers = new Dictionary<string, CounterHandler>();

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
            this.bot.OnCallbackQuery += BotOnInlineQueryReceived;
        }

        /// <summary>
        /// Запуск бота.
        /// </summary>
        internal void Start()
        {
            this.bot.StartReceiving();
        }

        // Получение собщения.
        private void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Logger.LogMessage($"{ e.Message.From.Id } { e.Message.From.FirstName } { e.Message.From.LastName } { e.Message.Text }");

            User user = new User
            {
                ChatId = e.Message.Chat.Id,
                UserId = e.Message.From.Id,
                Message = e.Message.Text,
                FirstName = e.Message.From.FirstName,
                LastName = e.Message.From.LastName,
                Username = e.Message.From.Username
            };

            if(e.Message.Type == MessageType.TextMessage)
                ProcessTextCommand(user);
        }

        // Обработать сообщение.
        private void ProcessTextCommand(User user)
        {
            CommandHandler handler;

            if(this.commandHandlers.TryGetValue(user.Message, out handler))
                handler(user, user.Message);
        }

        // Получение inline.
        private void BotOnInlineQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            User user = new User
            {
                ChatId = e.CallbackQuery.Message.Chat.Id,
                UserId = e.CallbackQuery.Message.From.Id,
                Message = e.CallbackQuery.Message.Text,
                MessageId = e.CallbackQuery.Message.MessageId,
                FirstName = e.CallbackQuery.Message.From.FirstName,
                LastName = e.CallbackQuery.Message.From.LastName,
                Username = e.CallbackQuery.Message.From.Username
            };

            // Проверка является ли inline переключаемым.
            if(e.CallbackQuery.Data.Contains("@"))
            {
                CounterHandler handler;

                string[] split = e.CallbackQuery.Data.Split('@');

                string callbackQueryData = split[0];

                Int16 count = Int16.Parse(split[1]);

                if(this.counterHandlers.TryGetValue(callbackQueryData, out handler))
                    handler(user, callbackQueryData, count);
            }
            else
            {
                CommandHandler handler;

                if(this.commandHandlers.TryGetValue(e.CallbackQuery.Data, out handler))
                    handler(user, e.CallbackQuery.Data);
            }
        }

        /// <summary>
        /// Добавить делегат в словарь.
        /// </summary>
        /// <param name="handler">Имя метода.</param>
        /// <param name="commandName">Команда.</param>
        public void AddCommandHandler(CommandHandler handler, string commandName)
        {
                this.commandHandlers[commandName] = handler;       
        }

        /// <summary>
        /// Добавить делегат переключателя в словарь.
        /// </summary>
        /// <param name="handler">Имя метода.</param>
        /// <param name="commandName">Команда.</param>
        public void AddCounterHandler(CounterHandler handler, string commandName, Int16 counter)
        {
            this.counterHandlers[commandName] = handler;
        }

        /// <summary>
        /// Отправить сообщение.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="message">Текст сообщения.</param>
        public async void SendText(long chatId, string message, IReplyMarkup button = null, ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = false)
        {
            await this.bot.SendChatActionAsync(chatId, ChatAction.Typing);

            await this.bot.SendTextMessageAsync(chatId, message, replyMarkup: button, parseMode: parseMode, disableWebPagePreview: disableWebPagePreview);
        }

        /// <summary>
        /// Отредактировать сообщение.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="messageid">Id сообщения.</param>
        /// <param name="message">Текст сообщения.</param>
        public void EditText(long chatId, int messageid, string message, IReplyMarkup button = null, ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = false)
        {
            bot.EditMessageTextAsync(chatId, messageid, message, replyMarkup: button, parseMode: parseMode, disableWebPagePreview: disableWebPagePreview);
        }

        /// <summary>
        /// Отправить изображение.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="photo">Ссылка на изображение.</param>
        public async void SendImage(long chatId, string photoLink, IReplyMarkup button = null)
        {
            await Task.Delay(500);

            await this.bot.SendChatActionAsync(chatId, ChatAction.UploadPhoto);

            await this.bot.SendPhotoAsync(chatId, new FileToSend(photoLink), replyMarkup: button);
        }

        /// <summary>
        /// Отправить стикер.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="photo">Ссылка на изображение.</param>
        public void SendSticker(long chatId, string photoLink, IReplyMarkup button = null, ParseMode parseMode = ParseMode.Default)
        {
             this.bot.SendChatActionAsync(chatId, ChatAction.Typing);

             this.bot.SendStickerAsync(chatId, new FileToSend(photoLink));
        }

        /// <summary>
        /// Отправить аудио.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="photo">Ссылка на изображение.</param>
        public async void SendAudio(long chatId, string audio, int duration, string performer,string title )
        {
            await this.bot.SendChatActionAsync(chatId, ChatAction.RecordAudio);

            await this.bot.SendAudioAsync(chatId, audio, duration, performer, title);
        }
    }
}
