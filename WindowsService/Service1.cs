using System;
using System.Timers;
using System.ServiceProcess;
using APIEntegrasyon.Models;
using APIEntegrasyon.Models.WindowsService;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer tmr = new System.Timers.Timer();

        protected override void OnStart(string[] args)
        {
            tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
            tmr.Interval = 6000;
            tmr.Start();
        }

        void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
        }

        protected override void OnContinue()
        {
            Helper helper = new Helper();
            base.OnContinue();

            if (Alarm.GetDateList().Contains(DateTime.Now))
            {
                foreach (var alarm in Alarm.postAlarms)
                {
                    if (alarm.DeadLine == DateTime.Now)
                    {
                        helper.SendTweet(alarm.Content);
                        Alarm.RemoveList(alarm);
                    }
                }
            }

            tmr.Enabled = true;
        }

        protected override void OnStop()
        {
            tmr.Enabled = false;
        }

        protected override void OnPause()
        {
            tmr.Enabled = false;
        }

        protected override void OnShutdown()
        {
            tmr.Enabled = false;
        }
    }
}
