using kudryavkaDiscordBot.discordClinet.Services.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot.discordClinet.Services.SiteAttendance
{
    public class TcateAccount : IAccount
    {
        public override Dictionary<string, string> GetUser()
        {
            throw new NotImplementedException();
        }

        public override void SetUser(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }

    public class Tcafe : IAttendance
    {
        public void Run(IAccount account)
        {
            throw new NotImplementedException();
        }
    }

}
