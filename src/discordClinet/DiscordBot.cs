﻿using Discord;
using Discord.Commands;
using Discord.Net.Queue;
using Discord.WebSocket;
using kudryavkaDiscordBot.discondClinet.Config;
using kudryavkaDiscordBot.discondClinet.Utility;
using kudryavkaDiscordBot.DiscondClinet.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot.DiscondClinet
{

    
    public class DiscordBot
    {
        /// <summary>
        /// Sington Value Initalize by ConfigurationHelper.Load()
        /// </summary>
        Configuration configuration;
        public DiscordBot()
        {
            this.configuration = ConfigurationHelper.Load();
            if(this.configuration == null || string.IsNullOrEmpty(this.configuration.DISCORD_TOKEN))
            {
                throw new Exception("Configuration loading failed or Discord token is missing.");
            }
        }

        public async Task Run()
        {
            using(var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();

                await InitializeAsync(services);
                client.Log += LogAsync;
                await client.LoginAsync(TokenType.Bot, this.configuration.DISCORD_TOKEN);
                await client.StartAsync();

                await Task.Delay(Timeout.Infinite);
            }
        }
        private async Task InitializeAsync(IServiceProvider services)
        {
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();
            await services.GetRequiredService<UserGuildJoinedService>().InitializeAsync();
        }
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<UserGuildJoinedService>()
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }
    }
}
