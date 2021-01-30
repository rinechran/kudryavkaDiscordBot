using System;
using System.Collections.Generic;
using System.Timers;

namespace JCronTab
{

    public class CronDaemon
    {
        private readonly System.Timers.Timer timer = new System.Timers.Timer(3000);
        private readonly List<ICronJob> CronJobs = new List<ICronJob>();
        private DateTime LastDate = DateTime.Now;
        public CronDaemon()
        {
            timer.AutoReset = true;
            timer.Elapsed += ExcuateElapsed;
        }

        public void AddJob(ICronJob Job)
        {
            CronJobs.Add(Job);
        }
        public void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            Abort();
            timer.Stop();
        }
        private void Abort()
        {
            foreach (CronJob job in CronJobs)
                job.Abort();
        }
        private void ExcuateElapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Minute == LastDate.Minute)
            {
                return;
            }
            LastDate = DateTime.Now;
            foreach (ICronJob job in CronJobs)
            {
                job.Excute(LastDate);
            }
        }



    }
}
