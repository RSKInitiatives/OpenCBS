using Common.Logging;
using OpenCBS.Services;
using Quartz;
using System;
using System.Linq;
using OpenCBS.Messaging.Email;
using OpenCBS.Messaging.Interfaces;
using OpenCBS.Services.Messaging;
using OpenCBS.Messaging.Custom;
using OpenCBS.Shared;
using OpenCBS.Messaging.SMS;
using OpenCBS.Enums;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;

namespace OpenCBS.SchedulerService.Jobs
{
    public class PasswordResetJob : IJob
    {
        private static ILog Log = LogManager.GetLogger("MP_WS_info");
        
        private static IServices ServiceProvider
        {
            get
            {
                return ServicesProvider.GetInstance();                
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Log.Debug("");
                Log.Info("Executing password reset job");

                IEmailSender _emailSender = new DefaultEmailSender();
                var UserService = ServiceProvider.GetUserServices();
                ApplicationSettingsServices generalSettingsService = ServiceProvider.GetApplicationSettingsServices();

                //Send reminders
                var users = UserService.FindAll(true)
                    .Where(user => 
                    (user.LastUpdated == null || user.LastUpdated <= DateTime.Now.AddDays(-20)) 
                    &&
                    (!user.IsExpired.HasValue || !user.IsExpired.Value));
                foreach (var user in users)
                {
                    try
                    {
                        ISms sms = new EstoreSms();
                        sms.UserName = Convert.ToString(generalSettingsService.SelectParameterValue(OGeneralSettings.SMS_GATEWAY_USERNAME));
                        sms.Password = Convert.ToString(generalSettingsService.SelectParameterValue(OGeneralSettings.SMS_GATEWAY_PASSWORD));

                        var daysRemaining = 30;// DateTime.Now - user.LastUpdated;
                        sms.Message = @"Hi " + user.Name + 
                            @". \nPlease ensure to update you password on OpenCBS within the next " + daysRemaining;
                        sms.AddRecipient(user.Phone);
                        sms.MessageFrom = "OpenCBS";
                        string response = sms.SendSms();                        
                        Log.Info(response);
                    }
                    catch (Exception exc)
                    {
                        Log.Error(string.Format("Error sending sms: {0}", exc.Message), exc);
                    }                    
                }

                //Expire user accounts
                users = UserService.FindAll(false).Where(user=>user.LastUpdated == null || user.LastUpdated <= DateTime.Now.AddDays(-30));
                foreach(var user in users)
                {
                    user.IsExpired = true;
                    user.TimedOut = true;
                    UserService.UpdateUserAccess(user);
                }
            }
            catch (Exception exc)
            {
                Log.Error(string.Format("Error resetting user passwords: {0}", exc.Message), exc);
            }
            finally
            {

            }
        }
    }
}
