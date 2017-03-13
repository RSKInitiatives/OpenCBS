using Common.Logging;
using OpenCBS.SchedulerService.Jobs;
using OpenCBS.Services;
using OpenCBS.Shared.Settings;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OpenCBS.SchedulerService
{
    public partial class Scheduler : ServiceBase
    {
        private ILog Log = LogManager.GetLogger("MP_WS_info");

        private static IScheduler scheduler;
        public Scheduler()
        {
            InitializeComponent();
        }

        private static IServices ServiceProvider
        {
            get
            {
                return ServicesProvider.GetInstance();
            }
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            //Debugger.Launch();
#endif

            Log.Info("Job factory startup task configuration");

            try
            {
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                //ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                //scheduler = schedulerFactory.GetScheduler();

                ApplicationSettingsServices generalSettingsService = ServiceProvider.GetApplicationSettingsServices();
                generalSettingsService.FillGeneralDatabaseParameter();
                Log.Debug("Application settings loaded");

                //IJobDetail InitializationJob = JobBuilder.Create<SMSChargeJob>().WithIdentity(typeof(InitializationJob).Name).Build();                
                IJobDetail SMSChargeJob = JobBuilder.Create<SMSChargeJob>().WithIdentity(typeof(SMSChargeJob).Name).Build();
                IJobDetail MessagingJob = JobBuilder.Create<MessagingJob>().WithIdentity(typeof(MessagingJob).Name).Build();
                IJobDetail ChequeCloseOutJob = JobBuilder.Create<ChequeCloseOutJob>().WithIdentity(typeof(ChequeCloseOutJob).Name).Build();

                IJobDetail PasswordResetJob = JobBuilder.Create<PasswordResetJob>().WithIdentity(typeof(PasswordResetJob).Name).Build();

                //ITrigger InitializationJobTrigger = TriggerBuilder.Create().WithIdentity("InitializationJobTrigger")
                //.StartNow().WithPriority(5).WithSimpleSchedule(s => s.WithRepeatCount(0).WithIntervalInMinutes(1)).Build();

                ITrigger SMSChargeJobTrigger = TriggerBuilder.Create().WithIdentity("SMSChargeJobTrigger")
                    .WithCronSchedule("0 0 1 ? * 6L").Build(); //Fires at 1am every last friday

                ITrigger MessagingJobTrigger = TriggerBuilder.Create().WithIdentity("MessagingJobTrigger")
                    .StartNow().WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever()
                    .WithMisfireHandlingInstructionNextWithExistingCount()).StartNow().Build();

                ITrigger ChequeCloseOutJobTrigger = TriggerBuilder.Create().WithIdentity("ChequeCloseOutJobTrigger")
                    /*.StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever()
                    .WithMisfireHandlingInstructionNextWithExistingCount()).StartNow()
                    .Build();*/
                    .WithCronSchedule("0 0 0 * * ?").Build(); // Fires at midnight everyday.

                ITrigger PasswordResetJobTrigger = TriggerBuilder.Create().WithIdentity("PasswordResetJobTrigger")
                    .WithCronSchedule("0 0 0 * * ?").Build(); // Fires at midnight everyday.


                //scheduler.ScheduleJob(InitializationJob, InitializationJobTrigger);
                scheduler.ScheduleJob(MessagingJob, MessagingJobTrigger);
                scheduler.ScheduleJob(SMSChargeJob, SMSChargeJobTrigger);
                scheduler.ScheduleJob(ChequeCloseOutJob, ChequeCloseOutJobTrigger);
                scheduler.ScheduleJob(PasswordResetJob, PasswordResetJobTrigger);
                scheduler.Start();

                Log.Info("Job factory started");
            }
            catch (SchedulerException se)
            {
                Log.Error(se);
            }
        }

        public void InstallService()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("OpenCBS Service Installer");
            Console.WriteLine("-------------------------");
            Console.WriteLine("-------------------------");

            if (OpenCBS.DatabaseConnection.Standard.CheckSQLDatabaseConnection())
            {
                Console.WriteLine("OpenCBS database detected");
                Console.WriteLine("-------------------------");
                return;
            }
            do
            {
                Console.WriteLine("Server: ");
                var input = Console.ReadLine();
                if (!String.IsNullOrEmpty(input))
                    TechnicalSettings.DatabaseServerName = input;
                Console.WriteLine("Database name: ");
                input = Console.ReadLine();
                if (!String.IsNullOrEmpty(input))
                    TechnicalSettings.DatabaseName = input;
                Console.WriteLine("Login name: ");
                input = Console.ReadLine();
                if (!String.IsNullOrEmpty(input))
                    TechnicalSettings.DatabaseLoginName = input;
                Console.WriteLine("Password: ");
                input = Console.ReadLine();
                if (!String.IsNullOrEmpty(input))
                    TechnicalSettings.DatabasePassword = input;
            }
            while (!OpenCBS.DatabaseConnection.Standard.CheckSQLDatabaseConnection());
        }

        public void UninstallService()
        {
            //throw new NotImplementedException();
        }

        protected override void OnStop()
        {
            if (scheduler != null && scheduler.IsStarted)
            {
                scheduler.Shutdown();
                Log.Debug("Job factory stopped");
            }
        }

        
    }
}
