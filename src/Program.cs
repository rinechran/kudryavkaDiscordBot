using Discord;
using Discord.WebSocket;
using kudryavkaDiscordBot.DiscondClinet;
using System;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot
{
    class Program
    {

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            DiscordBot bot = new DiscordBot();
            await bot.Run();
		}

    }
}
