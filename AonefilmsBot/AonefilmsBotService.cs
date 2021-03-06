﻿using System;
using System.Linq;
using System.Threading.Tasks;
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

        private const string start = "/start";
        private const string now = "🎬 Сейчас в кино";
        private const string online = "📲 Фильмы online";
        private const string news = "📣 Новости";
        private const string soon = "🔜 Скоро в кино";
        private const string pictures = "🎨 Картиночки";
        private const string help = "❓ Помощь";
        private const string best = "🔝 5 лучших";
        private const string random = "🔎 Случайный";
        private const string all = "📄 Все фильмы";
        private const string menu = "↩ Меню";
        private const string dolan = "💘 Доланчик";
        private const string dano = "💓 Дано";
        private const string stickers = "👻 Cтикеры";
        private const string nextrandom = "Random.txt";
        private const string nextbest = "TheBest.txt";
        private const string nextDano = "Dano.txt";

        /// <summary>
        /// Запустить службу.
        /// </summary>
        public void OnStart()
        {
            try
            {
                Logger.Initialize();

                Logger.LogMessage($"Логирование запущено.\n");

                Config.Load();

                Logger.LogMessage($"Конфигурация загружена.\n");


                bot = new AonefilmsBot();

                Int16 counter = 0;

                // Добавление делегатов.
                bot.AddCommandHandler(SendStart, start);
                bot.AddCommandHandler(SendNow, now);
                bot.AddCommandHandler(SendOnline, online);
                bot.AddCommandHandler(SendNews, news);
                bot.AddCommandHandler(SendSoon, soon);
                bot.AddCommandHandler(SendPictures, pictures);
                bot.AddCommandHandler(SendHelp, help);
                bot.AddCommandHandler(SendBest, best);
                bot.AddCommandHandler(SendRandom, random);
                bot.AddCommandHandler(SendAll, all);
                bot.AddCommandHandler(SendMenu, menu);
                bot.AddCommandHandler(SendStickers, stickers);
                bot.AddCommandHandler(SendDano, dano);
                bot.AddCommandHandler(SendDolan, dolan);

                bot.AddCounterHandler(SendNext, nextbest, counter);
                bot.AddCounterHandler(SendNextRandom, nextrandom, counter);
                bot.AddCounterHandler(SendNext, nextDano, counter);


                // Начать прием сообщений.
                bot.Start();

                Logger.LogMessage($"Запуск бота {bot.BotUsermame}\n");
            }
            catch(Exception exc)
            {
                Logger.LogError($"Ошибка запуска бота. {exc.ToString()}");

                throw;
            }
        }

        /// <summary>
        /// Остановить службу.
        /// </summary>
        public void OnStop()
        {
            Logger.LogMessage($"Бот остановлен.");
        }

        // Старт.
        private void SendStart(User user, string commmand)
        {
            // Запись нового пользователя.
            User.Add(user);

            string description = "Мы выбираем лучшие фильмы на крупнейших кинофестивалях и привозим их в Россию.\n\n" +

                $"Здесь мы собрали для тебя всю полезную и интересную информацию о наших проектах.\n\n" +

                $"Готов? Поехали! { Emoji.Victory }";

            bot.SendText(user.ChatId, description, Button.keyboardMain);
        }

        // Сейчас в кино.
        private void SendNow(User user, string commmand)
        {
            // Описание фильма.
            string description = $"<b>Патерсон</b> Джима Джармуша уже в кино!\n\nПатерсон(Адам Драйвер) — водитель автобуса, который замечает удивительные вещи. Он облачает красоту повседневности в стихи и посвящает их любимой жене Лоре (Голшифте Фарахани). В этой истории одной влюбленной пары главный поэт в кино Джим Джармуш создает настоящую оду современной жизни, где среди простых радостей и забот мерцает вечная красота.";

            bot.SendText(user.ChatId, description, parseMode: ParseMode.Html);

            // Постер фильма.
            string photoLink = "AgADAgAD26cxGw3aMElj07Et80D_ScZNtw0ABEkTfaTDy6MzrDUAAgI";

            bot.SendImage(user.ChatId, photoLink, Button.inlineBuy);
        }

        // Фильмы онлайн.
        private void SendOnline(User user, string commmand)
        {
            string description = $"Смотри наши фильмы в лицензионном качестве, без регистрации и смс! { Emoji.Eyes }";

            bot.SendText(user.ChatId, description, Button.keyboardOnline);
        }

        // Новости.
        private void SendNews(User user, string commmand)
        {
            string description = $"🍩 Участвуем в <a href=\"https://vk.com/patersonmovie?w=wall-134715766_308\">розыгрыше черно-белых призов</a> по фильму «Патерсон» Джима Джармуша!";
            
            bot.SendText(user.ChatId, description, parseMode: ParseMode.Html);
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
            string description = $"{ Emoji.HandDown } Выбери команду";

            bot.SendText(user.ChatId, description, Button.keyboardMem);
        }

        // Помощь.
        private void SendHelp(User user, string commmand)
        {
            string description = "Получай необходимую информацию с помощью расположенных ниже клавиш с командами\n\n" +

                $"Также ты подписан на рассылку горячих новостей и информации о мероприятиях. Отписаться можно командой /unsubscribe, но если станет скучно -верни командой /subscribe 🤗\n\n" +

                $"Мы в ВК: vk.com/a_onefilms\n" +

                $"Мы в Instagram: instagram.com/aonefilms\n\n" +

                $"🆘 Если в работе бота есть ошибка или возникла проблема, пиши сюда: @VikaLymar";

            bot.SendText(user.ChatId, description, disableWebPagePreview: true);
        }

        // Меню.
        private void SendMenu(User user, string commmand)
        {
            string description = $"{ Emoji.HandDown } Выбери команду";

            bot.SendText(user.ChatId, description, Button.keyboardMain);
        }

        // Все фильмы.
        private void SendAll(User user, string commmand)
        {
            string photoLink = "AgADAgADe6gxG0xXhRGlThqoUSHMbybMgQ0ABFjc80FgApvPBeACAAEC";

            bot.SendImage(user.ChatId, photoLink, Button.inlineAllFilms);
        }

        // Стикеры.
        private void SendStickers(User user, string commmand)
        {
            // Id стикеров.
            string[] stickerId = { "BQADAgAD0AADzqH2A5odTXylXj1-Ag", "BQADAgADjAADzqH2AyC-AAEJSzJSGwI", "BQADAgADuAADzqH2Awy-er0PcSJTAg", "BQADAgADbAADzqH2A2WDdCvWzmOzAg", "BQADAgADQgADzqH2A_hZhgwTywABxwI" };

            Random rnd = new Random();

            // Отправить случайный стикер и список стикеров.
            bot.SendSticker(user.ChatId, stickerId[rnd.Next(0, 5)]);

            string description = $"<a href=\"https://telegram.me/addstickers/benechka\">💙 Беня Камбербэтчик</a>\n" +

                "<a href=\"https://telegram.me/addstickers/makovochka\">💚 Джеймс Маковочка</a>\n" +

                "<a href=\"https://telegram.me/addstickers/fassenka\">💛 Майкл Фасси Фассбендерчик</a>\n" +

                "<a href=\"https://telegram.me/addstickers/garrelik\">💜 Типичный Гаррелюшка</a>\n" +

                "<a href=\"https://telegram.me/addstickers/danopirozhok\">❤ Пирожочек Дано</a>";

            bot.SendText(user.ChatId, description, parseMode: ParseMode.Html);
        }

        // Случайный фильм.
        private void SendRandom(User user, string commmand)
        {
            Random rnd = new Random();

            // Count - номер случайной строки в Random.txt; Line - случайная строка.
            Int32 count = System.IO.File.ReadAllLines(Config.randomFilmFile).Count();

            string[] line = System.IO.File.ReadAllLines(Config.randomFilmFile)[rnd.Next(0, count)].Split('@');

            // Отправить случайный фильм.
            string description = $"<a href=\"{ line[0] }\">{ line[1] }</a>\n\n";

            bot.SendText(user.ChatId, description, Button.inlineNextRandom, ParseMode.Html);
        }

        // // Следующий случайный фильм
        private void SendNextRandom(User user, string filename, Int16 counter)
        {
            Random rnd = new Random();

            // Count - номер случайной строки в Random.txt; Line - случайная строка.
            Int32 count = System.IO.File.ReadAllLines(Config.botConfigPath + filename).Count();

            string[] line = System.IO.File.ReadAllLines(Config.botConfigPath + filename)[rnd.Next(0, count)].Split('@');

            // Отправить следующий случайный фильм.
            string description = $"<a href=\"{ line[0] }\">{ line[1] }</a>\n\n";

            bot.EditText(user.ChatId, user.MessageId, description, Button.inlineNextRandom, parseMode: ParseMode.Html);
        }

        // 5 лучших.
        private void SendBest(User user, string commmand)
        {
            // Line - случайная строка.
            string[] line = System.IO.File.ReadAllLines(Config.bestFilmFile)[0].Split('@');

            // Отправить лучший фильм.
            string description = $"<a href=\"{ line[0] }\">{ line[1] }</a>\n\n";

            bot.SendText(user.ChatId, description, Button.inlineNextBestFilm, ParseMode.Html);
        }

        // Следующий лучший фильм или герой.
        private void SendNext(User user, string filename, Int16 counter)
        {
            Random rnd = new Random();

            counter++;

            // Обнулить счетчик.
            if(counter == System.IO.File.ReadAllLines(Config.botConfigPath + filename).Count())
                counter = 0;

            if(counter == 5 && filename == "Dano.txt")
                bot.SendAudio(user.ChatId, "BQADAgADQgADTFeFEZP9nIkSDkS7Ag", 93 , "Paul Dano","God Only Knows");

            string[] line = System.IO.File.ReadAllLines(Config.botConfigPath + filename)[counter].Split('@');

            InlineKeyboardMarkup inlineNextBestFilm = new InlineKeyboardMarkup(new[] {
                    new[] { new InlineKeyboardButton("Следующий →", filename + "@" + counter) }
                    });

            string description;

            // Отображение записи.
            if(filename.Contains("Dano") || filename.Contains("Dolan") || filename.Contains("Paterson"))
                 description = $"{ line[1] }\n\n<a href=\"{ line[0] }\">\U000000A0</a>\n\n";
            else
                description = $" <a href=\"{ line[0] }\">{ line[1] }</a>\n\n";

            bot.EditText(user.ChatId, user.MessageId, description, inlineNextBestFilm, parseMode: ParseMode.Html);
        }

        // Долан.
        private void SendDolan(User user, string commmand)
        {
            string description = "Кнопка в ремонте.";

            bot.SendText(user.ChatId, description);
        }

        // Дано.
        private void SendDano(User user, string commmand)
        {
            // Line - случайная строка.
            string[] line = System.IO.File.ReadAllLines(Config.DanoFile)[0].Split('@');

            // Отправить лучший фильм.
            string description = $"{ line[1] }\n\n<a href=\"{ line[0] }\">\U000000A0</a>\n\n";

            bot.SendText(user.ChatId, description, Button.inlineDano, ParseMode.Html);
        }
    }
}