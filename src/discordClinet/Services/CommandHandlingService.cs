using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot.DiscondClinet.Services
{
    public class CommandHandlingService
    {
        private readonly CommandService command;
        private readonly DiscordSocketClient discord;
        private readonly IServiceProvider services;
        public CommandHandlingService(IServiceProvider services)
        {
            command = services.GetRequiredService<CommandService>();
            discord = services.GetRequiredService<DiscordSocketClient>();
            this.services = services;


            discord.MessageReceived += MessageReceivedAsync;

        }

        public async Task InitializeAsync()
        {
            await command.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }
        public async Task MessageReceivedAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null)
            {
                return;
            }
            if(message.Author.IsBot == true)
            {
                return;
            }

            int argPos = 0;
            if (!message.HasCharPrefix('~', ref argPos)){
                return;
            }

            SocketCommandContext context = new SocketCommandContext(discord, message);
            await command.ExecuteAsync(context, argPos, services);
        }
    }
}
