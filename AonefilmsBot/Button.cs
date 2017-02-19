using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AonefilmsBot
{
    /// <summary>
    /// Кнопки для бота.
    /// </summary>
    static class Button
    {
        /// <summary>
        /// Главное меню.
        /// </summary>
        public static ReplyKeyboardMarkup keyboardMain = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton($"{ Emoji.ClapperBoard } Сейчас в кино"), new KeyboardButton($"{ Emoji.MobileWithArrow } Фильмы online") },
            new[] { new KeyboardButton($"{ Emoji.Megaphone } Новости"), new KeyboardButton($"{ Emoji.Soon } Скоро в кино") },
            new[] { new KeyboardButton($"{ Emoji.ArtistPalette } Картиночки"), new KeyboardButton($"{ Emoji.Question } Помощь") }
        }, resizeKeyboard: true);

        /// <summary>
        /// Меню Фильмы online.
        /// </summary>
        public static ReplyKeyboardMarkup keyboardOnline = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton($"{ Emoji.TopArrow } 5 лучших"), new KeyboardButton($"{ Emoji.MagnifyingGlass } Случайный") },
            new[] { new KeyboardButton($"{ Emoji.PageFacingUp } Все фильмы"), new KeyboardButton($"{ Emoji.ArrowCurving } Меню") }
        }, resizeKeyboard: true);

        /// <summary>
        /// Меню Картиночки.
        /// </summary>
        public static ReplyKeyboardMarkup keyboardMem = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton("\U0001F498 Доланчик"), new KeyboardButton("\U0001F493 Дано") },
            new[] { new KeyboardButton("\U0001F47B Cтикеры"), new KeyboardButton($"{ Emoji.ArrowCurving } Меню") }
        }, resizeKeyboard: true);

        /// <summary>
        /// Inline переключение случайных фильмов
        /// </summary>
        public static InlineKeyboardMarkup inlineNextRandom = new InlineKeyboardMarkup(new[] {
                new[] { new InlineKeyboardButton("Следующий фильм →", "Следующий рандомный") }
        });

        /// <summary>
        /// Inline покупка билета в кинотеатре.
        /// </summary>
        public static InlineKeyboardMarkup inlineBuy = new InlineKeyboardMarkup(new[] {
            new InlineKeyboardButton {
                Text = "Приобрести билеты",
                Url = "https://goo.gl/tKAkWz"}
        });

        /// <summary>
        /// Inline все фильмы (Кинопоиск).
        /// </summary>
        public static InlineKeyboardMarkup inlineAllFilms = new InlineKeyboardMarkup(new[] {
            new InlineKeyboardButton {
                Text = "Перейти →",
                Url = "https://www.kinopoisk.ru/lists/m_act[company]/174/m_act[all]/ok/",
                CallbackData = "Смотреть" }
        });

        /// <summary>
        /// Inline переключение лучших фильмов.
        /// </summary>
        public static InlineKeyboardMarkup inlineNextBestFilm = new InlineKeyboardMarkup(new[] {
            new[] { new InlineKeyboardButton("Следующий →", "1") }
        });
    }
}
