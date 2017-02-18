using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AonefilmsBot
{
    /// <summary>
    /// Пользователь бота.
    /// </summary>
    class User
    {
        public long UserId { get; set; }

        public long ChatId { get; set; }

        public string Message { get; set; }

        /// <summary>
		/// Добавляет нового пользователя.
		/// </summary>
		/// <param name="user"></param>
		internal static void Add(User user)
        {
            //lock(List)
            //{
            //    List.Add(user);

            //    Save();
            //}
        }
    }
}
