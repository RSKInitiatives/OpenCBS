using Common.Logging;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Enums;
using OpenCBS.Services;
using OpenCBS.Services.Events;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCBS.Messaging.Custom;

namespace OpenCBS.SchedulerService.Jobs
{
    public class ChequeCloseOutJob : IJob
    {
        private ILog Log = LogManager.GetLogger("MP_WS_info");

        private static IServices ServiceProvider
        {
            get { return ServicesProvider.GetInstance(); }
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Log.Info("Executing cheque close out job");

                Log.Info("");
                Log.Info("-------------------------------------");

                var generalSettingsService = ServiceProvider.GetApplicationSettingsServices();

                int automatedProcess = Convert.ToInt32(generalSettingsService.SelectParameterValue(OGeneralSettings.AUTOMATE_CHEQUE_DEPOSIT));

                if (automatedProcess == 1)
                {
                    SavingEventServices savingEventService = ServiceProvider.GetSavingEventServices();
                    AccountServices savingService = ServiceProvider.GetAccountServices();
                    UserServices _userService = ServiceProvider.GetUserServices();

                    List<SavingEvent> savingEvents = savingEventService.SelectByCode(OSavingEvents.PendingDeposit);

                    foreach (var savingEvent in savingEvents)
                    {
                        DateTime threeDaysAgo = DateTime.Now.AddWorkDays(-3);
                        DateTime fiveDaysAgo = DateTime.Now.AddWorkDays(-5);

                        if (savingEvent is SavingPendingDepositEvent && !savingEvent.Deleted && savingEvent.IsPending)
                        {
                            try
                            {
                                if (
                                    (savingEvent.IsLocal.HasValue && savingEvent.IsLocal.Value && savingEvent.Date <= threeDaysAgo)
                                    ||
                                    (savingEvent.IsLocal.HasValue && !savingEvent.IsLocal.Value && savingEvent.Date <= fiveDaysAgo)
                                    )
                                {
                                    var _saving = savingService.GetSaving(savingEvent.ContracId);
                                    var adminUser = _userService.Find(1);

                                    savingService.Deposit(_saving, TimeProvider.Now,
                                        savingEvent.Amount, savingEvent.Description, savingEvent.ReferenceNumber,
                                        adminUser, false, savingEvent.IsLocal, (OSavingsMethods)savingEvent.SavingsMethod,
                                        new PaymentMethod(), savingEvent.Id, Teller.CurrentTeller);

                                    savingService.ChangePendingEventStatus(savingEvent.Id, false);
                                }
                            }
                            catch (Exception exc)
                            {
                                Log.Error(string.Format("Error closing cheque deposit: {0}", exc.Message), exc);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Error(exc);
            }
        }
    }
}
