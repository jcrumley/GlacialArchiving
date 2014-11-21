using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using TBADev.MVC.ClerkOperations;
using TBADev.MVC.Tools;
using GlacialArchiving.DataAccess.Repositories;

namespace GlacialArchiving.ClerkService
{
    public partial class Service : ServiceBase
    {
        #region Property 'ClerkServiceName'
        public static string ClerkServiceName { get { return ConfigurationManager.AppSettings["ClerkServiceName"]; } }
        #endregion
        #region Property 'timer'
        private Timer timer;
        #endregion
        #region Property 'Clerk'
        private ClerkHandler m_Clerk = null;
        private ClerkHandler Clerk
        {
            get
            {
                if (m_Clerk == null)
                    m_Clerk = new ClerkHandler();
                return m_Clerk;
            }
        }
        #endregion

        public Service()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            int tmpCnt = this.Clerk.ThingsToDo.Count; //forces init on start up
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = true;
            timer.Interval = GetWaitPeriod();
            timer.Start();

            WriteEventEntry("Process has been started", EventLogEntryType.Information);
            SendEmail("Process has been started");
        }
        protected override void OnStop()
        {
            WriteEventEntry("Process has been stopped", EventLogEntryType.Warning);
            SendEmail("Process has been stopped");
        }

        protected void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                System.Threading.Thread.Sleep(1000 * 5); //sleeps for 5 seconds
                this.Clerk.ProcessForTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                SendEmail(Utilities.CreateMessage(ex));
            }

            timer.Interval = GetWaitPeriod();
            timer.Start();
        }

        public static void WriteEventEntry(string subject, EventLogEntryType type)
        {
            EventLog.WriteEntry(ClerkServiceName + " Log", subject.Replace("<br />", "\n").Replace("<br>", "\n"), type);
        }
        
        private double GetWaitPeriod()
        {
            DateTime now = DateTime.Now;
            double waitPeriod = 0;
            double.TryParse(ConfigurationManager.AppSettings["WaitPeriodInSecs"], out waitPeriod);
            if (waitPeriod == 0)
                waitPeriod = 60;

            DateTime nextRun = now.AddSeconds(waitPeriod);
            nextRun = Convert.ToDateTime(nextRun.ToString("MM/dd/yyyy hh:mm tt")); //Makes it on the minute (seconds will be 0)

            return nextRun.Subtract(now).TotalMilliseconds;
        }

        public static void SendEmail(string body)
        {
            SendEmail(ClerkServiceName + " on " + Environment.MachineName, body);
        }
        public static void SendEmail(string subject, string body)
        {
            string toEmail = new SettingRepository().ErrorEmail;
            Utilities.SendEmail(toEmail, subject, body);
        }
    }
}
