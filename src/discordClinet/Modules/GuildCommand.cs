using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace kudryavkaDiscordBot.DiscondClinet.Modules
{
    public class GuildCommand : ModuleBase<SocketCommandContext>
    {

        [Command("tcafe")]
        public async Task TranslateJapenTokorea([Remainder] string text)
        {
            string trimText = text.Trim();
            if (string.IsNullOrEmpty(trimText))
            {
                return;
            }

            string result = $"> {Context.User} : {text}\n";
            await ReplyAsync(result);
            return;
        }
    }
}
