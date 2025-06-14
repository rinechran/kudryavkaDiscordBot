using Discord;
using Discord.WebSocket;
using kudryavkaDiscordBot.DiscondClinet;
using System;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot
{
    class Program
    {

        public static async Task Main(string[] args)
            => await new Program().MainAsync();

        public async Task MainAsync()
        {
            DiscordBot bot = new DiscordBot();
            await bot.Run();
		}

    }
}
