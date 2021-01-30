using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot.DiscondClinet.Modules
{
    public class DmCommand : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [RequireContext(ContextType.DM)]
        public Task PingAsync()
        {
            string helpResult = $"```#jp -> translate japen -> korea \n #kj -> translate kroea -> japem```";
            return ReplyAsync(helpResult);
        }

    }
}
