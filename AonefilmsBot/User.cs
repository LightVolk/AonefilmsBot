using System;
using System.Data.SQLite;

namespace AonefilmsBot
{
    /// <summary>
    /// Пользователь бота.
    /// </summary>
    class User
    {
        public long ChatId { get; set; }

        public long UserId { get; set; }

        public string Message { get; set; }

        public int MessageId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        /// <summary>
		/// Добавляет нового пользователя.
		/// </summary>
		/// <param name="user"></param>
		internal static void Add(User user)
        {
            if(!System.IO.File.Exists("USERS.db"))
                SQLiteConnection.CreateFile("USERS.db");

            using(var connection = new SQLiteConnection("Data Source=USERS.db"))
            {
                connection.Open();

                using(var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                        @"create table if not exists [USERS](
                            [id] integer not null primary key,
                            [firstname] string null,
                            [lastname] string null,
                            [username] string null,
                            [subscribe] bool null,
                            [date] integer null
                        );";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"INSERT OR REPLACE INTO [USERS] (id,firstname,lastname,username,subscribe,date) values(:id,:firstname,:lastname,:username,:subscribe,:date)";
                    cmd.Parameters.AddWithValue("id", user.ChatId);
                    cmd.Parameters.AddWithValue("firstname", user.FirstName);
                    cmd.Parameters.AddWithValue("lastname", user.LastName);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("subscribe", 1);
                    cmd.Parameters.AddWithValue("date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
