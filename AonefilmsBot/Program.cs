using System;

namespace AonefilmsBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new AonefilmsBotService();

            service.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();       
        }
    }
}
