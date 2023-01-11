using SatisfactoryBot.Core;

namespace SatisfactoryBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bot.Run().GetAwaiter().GetResult();
        }
    }
}