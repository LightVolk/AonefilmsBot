using Newtonsoft.Json;
using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AonefilmsBot
{
    /// <summary>
    /// Служба запуска AonefilmsBot.
    /// </summary>
    class AonefilmsBotService
    {
        private AonefilmsBot bot;

        private const string start = "/START";
        private const string now = "СЕЙЧАС";
        private const string online = "ФИЛЬМЫ";
        private const string news = "НОВОСТИ";
        private const string soon = "СКОРО";
        private const string pictures = "КАРТИН";
        private const string help = "ПОМОЩЬ";
        private const string five = "5";
        private const string random = "СЛУЧА";
        private const string all = "ВСЕ";
        private const string menu = "МЕНЮ";
        private const string dolan = "ДОЛАН";
        private const string dano = "ДАНО";
        private const string stickers = "СТИКЕР";

        // Главное меню.
        private static ReplyKeyboardMarkup keyboardMain = new ReplyKeyboardMarkup(new[] {
                new[] { new KeyboardButton($"{ Emoji.ClapperBoard } Сейчас в кино"), new KeyboardButton($"{ Emoji.MobileWithArrow } Фильмы online") },
                new[] { new KeyboardButton($"{ Emoji.Megaphone } Новости"), new KeyboardButton($"{ Emoji.Soon } Скоро в кино") },
                new[] { new KeyboardButton($"{ Emoji.ArtistPalette } Картиночки"), new KeyboardButton($"{ Emoji.Question } Помощь") }
        }, resizeKeyboard: true);

        // Меню Фильмы online.
        private static ReplyKeyboardMarkup keyboardOnline = new ReplyKeyboardMarkup(new[] {
                    new[] { new KeyboardButton($"{ Emoji.TopArrow } 5 лучших"), new KeyboardButton($"{ Emoji.MagnifyingGlass } Случайный") },
                    new[] { new KeyboardButton($"{ Emoji.PageFacingUp } Все фильмы"), new KeyboardButton($"{ Emoji.ArrowCurving } Меню") }
        }, resizeKeyboard: true);

        private static ReplyKeyboardMarkup memKeyboard = new ReplyKeyboardMarkup(new[] {
                    new[] { new KeyboardButton("\U0001F498 Доланчик"), new KeyboardButton("\U0001F493 Дано") },
                    new[] { new KeyboardButton("\U0001F47B Cтикеры"), new KeyboardButton("\U000021A9 Меню") }
        }, resizeKeyboard: true);

        // Inline переключение случайных фильмов.
        private static InlineKeyboardMarkup nextRandomFilm = new InlineKeyboardMarkup(new[] {
                new[] { new InlineKeyboardButton("Следующий фильм →", "Следующий рандомный") }
        });

        // Inline-кнопка покупки билета в кинотеатре.
        private static InlineKeyboardMarkup inlineBuy = new InlineKeyboardMarkup(new[] {
                            new InlineKeyboardButton {
                            Text = "Приобрести билеты",
                            Url = "https://goo.gl/tKAkWz"}
        });
 
        /// <summary>
        /// Запустить службу.
        /// </summary>
        public void OnStart()
        {
            try
            {
                // Загрузить конфигурацию бота.
                Config.Load();

                bot = new AonefilmsBot();
                
                // Добавление делегатов.
                bot.AddCommandHandler(SendStart, start);
                bot.AddCommandHandler(SendNow, now);
                bot.AddCommandHandler(SendOnline, online);
                bot.AddCommandHandler(SendNews, news);
                bot.AddCommandHandler(SendSoon, soon);
                bot.AddCommandHandler(SendPictures, pictures);
                bot.AddCommandHandler(SendHelp, help);
                bot.AddCommandHandler(SendStart, five);
                bot.AddCommandHandler(SendStart, random);
                bot.AddCommandHandler(SendStart, all);
                bot.AddCommandHandler(SendStart, menu);
                bot.AddCommandHandler(SendStart, dolan);
                bot.AddCommandHandler(SendStart, dano);
                bot.AddCommandHandler(SendStart, stickers);

                // Начать прием сообщений.
                bot.Start();

                Console.WriteLine($"Бот запущен.");
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Ошибка запуска бота. { exc }");
                throw;
            }
        }

        /// <summary>
        /// Остановить службу.
        /// </summary>
        public void OnStop()
        {
            Console.WriteLine($"Бот остановлен.");
        }

        // Старт.
        private void SendStart(User user, string commmand)
        {
            // Запись информации о пользователе
            // TelegaBot.User.ToWriteUserInfo(message);

            // Отправить приветственное сообщение.
            if(System.IO.File.Exists(@"Text\Start.txt"))
                bot.SendText(user.ChatId, System.IO.File.ReadAllText(@"Text\Start.txt"), keyboardMain); 
            else
               bot.SendText(user.ChatId, $"Новости в печати, ожидайте! { Emoji.Memo }", keyboardMain);
        }

        // Сейчас в кино.
        private void SendNow(User user, string commmand)
        {
            // Описание фильма.
            string description = $"<b>Патерсон</b> Джима Джармуша уже в кино!\n\nПатерсон(Адам Драйвер) — водитель автобуса, который замечает удивительные вещи. Он облачает красоту повседневности в стихи и посвящает их любимой жене Лоре (Голшифте Фарахани). В этой истории одной влюбленной пары главный поэт в кино Джим Джармуш создает настоящую оду современной жизни, где среди простых радостей и забот мерцает вечная красота.";
            bot.SendText(user.ChatId, description, parseMode: ParseMode.Html);

            // Постер фильма.
            string photoLink = "AgADAgAD26cxGw3aMElj07Et80D_ScZNtw0ABEkTfaTDy6MzrDUAAgI";
            bot.SendImage(user.ChatId, photoLink, inlineBuy);
        }

        // Фильмы онлайн.
        private void SendOnline(User user, string commmand)
        {
            string description = $"Смотри наши фильмы в лицензионном качестве, без регистрации и смс! { Emoji.Eyes }";
            bot.SendText(user.ChatId, description, keyboardOnline);
        }

        // Новости.
        private void SendNews(User user, string commmand)
        {
            string description = $"🍩 Участвуем в <a href=\"https://vk.com/patersonmovie?w=wall-134715766_308\" >розыгрыше черно-белых призов</a> по фильму «Патерсон» Джима Джармуша!\n\n24 февраля Антон Долин приедет в Питер, чтобы представить свою книгу о Джиме Джармуше! Скоро сообщим подробности 😉";
            bot.SendText(user.ChatId, description, parseMode: ParseMode.Html);

            string photoLink = "AgADAgAD3qcxGw3aMEmPKbJwJ38hxLBKtw0ABKaJKUwMrxNaPjkAAgI";
            bot.SendImage(user.ChatId, photoLink);
        }

        // Скоро в кино.
        private void SendSoon(User user, string commmand)
        {
            // Описание фильма.
            string description = "23 марта мы выпускаем новый фильм <b>Король бельгийцев</b> 👑\n\nКомедия о балканском путешествии короля Бельгии, которому нужно срочно снять фильм о собственной персоне и спасти страну от раскола.\n\nПремьера роуд-муви состоялась на Международном Венецианском кинофестивале в 2016 году.";
            bot.SendText(user.ChatId, description, parseMode: ParseMode.Html);

            // Постер фильма.
            string photoLink = "AgADAgAD3acxGw3aMEkehNQtGIkgUYs_tw0ABOcedO8NIUWuuDcAAgI";
            bot.SendImage(user.ChatId, photoLink);
        }

        // Картиночки.
        private void SendPictures(User user, string commmand)
        {
            string description = $"\U0001F447 Выбери команду";
            bot.SendText(user.ChatId, description, memKeyboard);
        }

        // Помощь.
        private void SendHelp(User user, string commmand)
        {
            string description = "Получай необходимую информацию с помощью расположенных ниже клавиш с командами.\n\n" +

            "Также ты подписан на рассылку горячих новостей и информации о мероприятиях.Отписаться можно командой /unsubscribe, но если станет скучно -верни командой /subscribe 🤗\n\n" +

            "Мы в ВК: vk.com/a_onefilms\n" +
            "Мы в Instagram: instagram.com/aonefilms\n\n" +

            "🆘 Если в работе бота есть ошибка или возникла проблема, пиши сюда: @VikaLymar\n";
            bot.SendText(user.ChatId, description, memKeyboard,disableWebPagePreview: true);
        }





    }
}
