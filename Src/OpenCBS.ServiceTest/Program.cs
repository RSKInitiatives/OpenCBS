using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Enums;
using OpenCBS.Services;
using OpenCBS.Services.Events;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCBS.Messaging.Custom;

namespace OpenCBS.ServiceTest
{
    class Program
    {
        private static IServices ServiceProvider
        {
            get { return ServicesProvider.GetInstance(); }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Executing cheque close out job");
            
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------");
            
            var generalSettingsService = ServicesProvider.GetInstance().GetApplicationSettingsServices();
            generalSettingsService.FillGeneralDatabaseParameter();

            int automatedProcess = Convert.ToInt32(ApplicationSettings.GetInstance("").GetSpecificParameter(OGeneralSettings.AUTOMATE_CHEQUE_DEPOSIT));

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
                            Console.WriteLine(string.Format("Error closing cheque deposit: {0}", exc.Message), exc);
                        }
                    }
                }
            }
        }
    }
}
