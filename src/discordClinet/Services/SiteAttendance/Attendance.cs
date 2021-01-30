using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kudryavkaDiscordBot.discordClinet.Services.Attendance
{

    public abstract class IAccount{

        Dictionary<string, string> _data = new Dictionary<string, string>();

        public abstract void SetUser(Dictionary<string, string> data);

        public abstract Dictionary<string, string> GetUser();
    }

    public interface IAttendance
    {
        void Run(IAccount account);
    }



}
