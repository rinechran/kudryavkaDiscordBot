using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCronTab
{
    public interface ICronJob
    {
        public void Excute(DateTime date);
        public void Abort();
    }
    public class CronJob : ICronJob
    {
        private readonly ICronSchedule cronSchelude;
        private ThreadStart ThreadFunction;
        private Thread OwnThread;
        public CronJob(string schedule , ThreadStart threadStart)
        {
            cronSchelude = new CronSchedule(schedule);
            ThreadFunction = threadStart;
            OwnThread = new Thread(ThreadFunction);
        }
        public void Abort() => OwnThread.Abort();

        public void Excute(DateTime date)
        {
            if (cronSchelude.IsTime(date) == false)
                return;

            if (OwnThread.ThreadState == ThreadState.Running)
                return;
            OwnThread = new Thread(ThreadFunction);
            OwnThread.Start();
        }
    }
}
