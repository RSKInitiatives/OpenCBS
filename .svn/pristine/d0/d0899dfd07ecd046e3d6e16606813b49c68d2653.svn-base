// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using OpenCBS.Messaging.Email;
using OpenCBS.Messaging.Interfaces;
using OpenCBS.Services;
using OpenCBS.Services.Messaging;
using System;
using OpenCBS.Messaging.Custom;
using System.Linq;
using OpenCBS.Shared;
using OpenCBS.Messaging.SMS;
using OpenCBS.Enums;
using System.Collections;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Messaging
{
    static class Program
    {
        private static IServices ServiceProvider
        {
            get { return ServicesProvider.GetInstance(); }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] pArgs)
        {
            IEmailSender _emailSender = new DefaultEmailSender();
            QueuedEmailServices _queuedEmailService = ServiceProvider.GetQueuedEmailServices();
            QueuedSMSServices _queuedSMSService = ServiceProvider.GetQueuedSMSServices();

            var generalSettingsService = ServicesProvider.GetInstance().GetApplicationSettingsServices();
            generalSettingsService.FillGeneralDatabaseParameter();

            Console.WriteLine("Executing queued message send job");

            var sendTriesStart = 0;
            var sendTriesEnd = OpenCBSConstants.MessagingMaxSentTries;
            var queuedEmails = _queuedEmailService.SearchEmails(null, null, null, null, true, sendTriesStart, sendTriesEnd, false, 0, 10000);
            var queuedsms = _queuedSMSService.SearchSMSs(null, null, null, null, true, sendTriesStart, sendTriesEnd, false, 0, 10000);
            
            
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Sending SMS");
            Console.WriteLine("Queued sms count: " + queuedsms.Count);

            foreach (var qe in queuedsms)
            {                
                try
                {                      
                    ISms sms = new EstoreSms();
                    sms.UserName = Convert.ToString(ApplicationSettings.GetInstance("").GetSpecificParameter(OGeneralSettings.SMS_GATEWAY_USERNAME));
                    sms.Password = Convert.ToString(ApplicationSettings.GetInstance("").GetSpecificParameter(OGeneralSettings.SMS_GATEWAY_PASSWORD));

                    sms.Message = qe.Message;
                    sms.AddRecipient(qe.Recipient);
                    sms.MessageFrom = qe.From;
                    qe.Response = sms.SendSms();

                    if(qe.Response.ToLower().Contains("ok"))
                        qe.SentOnUtc = DateTime.UtcNow;

                    Console.WriteLine(qe.Response);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(string.Format("Error sending sms: {0}", exc.Message), exc);
                }
                finally
                {
                    qe.SentTries = qe.SentTries + 1;
                    _queuedSMSService.Update(qe);
                }
            }

            Console.WriteLine(""); 
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Sending Emails");
            Console.WriteLine("Queued email count: " + queuedEmails.Count);
            
            foreach (var qe in queuedEmails)
            {
                var bcc = String.IsNullOrWhiteSpace(qe.Bcc)
                            ? null
                            : qe.Bcc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var cc = String.IsNullOrWhiteSpace(qe.CC)
                            ? null
                            : qe.CC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    var smtpContext = new SmtpContext(qe.EmailAccount);

                    var msg = new EmailMessage(
                        new EmailAddress(qe.To, qe.ToName),
                        qe.Subject,
                        qe.Body,
                        new EmailAddress(qe.From, qe.FromName));

                    if (qe.ReplyTo.HasValue())
                    {
                        msg.ReplyTo.Add(new EmailAddress(qe.ReplyTo, qe.ReplyToName));
                    }

                    if (cc != null)
                        msg.Cc.AddRange(cc.Where(x => x.HasValue()).Select(x => new EmailAddress(x)));

                    if (bcc != null)
                        msg.Bcc.AddRange(bcc.Where(x => x.HasValue()).Select(x => new EmailAddress(x)));

                    _emailSender.SendEmail(smtpContext, msg);

                    qe.SentOnUtc = DateTime.UtcNow;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(string.Format("Error sending e-mail: {0}", exc.Message), exc);
                }
                finally
                {
                    qe.SentTries = qe.SentTries + 1;
                    _queuedEmailService.Update(qe);
                }
            }
            Console.WriteLine("Queued message send completed");
        }
    }
}
