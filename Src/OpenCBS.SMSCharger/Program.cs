using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Messaging;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Services;
using OpenCBS.Services.Messaging;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.SMSCharger
{
    static class Program
    {
        private static IServices ServiceProvider
        {
            get { return ServicesProvider.GetInstance(); }
        }

        [STAThread]
        static void Main(string[] args)
        {
            QueuedSMSServices _queuedSMSService = ServiceProvider.GetQueuedSMSServices();
            UserServices _userService = ServiceProvider.GetUserServices();
            ClientServices _clientService = ServiceProvider.GetClientServices();
            SavingServices _savingService = ServiceProvider.GetSavingServices();
            PaymentMethodServices _paymentMethodService = ServiceProvider.GetPaymentMethodServices();

            var generalSettingsService = ServicesProvider.GetInstance().GetApplicationSettingsServices();
            generalSettingsService.FillGeneralDatabaseParameter();

            Console.WriteLine("Executing sms charges job");

            var queuedsms = _queuedSMSService.GetUncharged();

            Console.WriteLine("");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Charging SMS");
            Console.WriteLine("Uncharged sms count: " + queuedsms.Count);
            
            decimal smsCharge = Convert.ToDecimal(ApplicationSettings.GetInstance("").GetSpecificParameter(OGeneralSettings.SMS_CHARGE));
            
            var adminUser = _userService.Find(1);
            var paymentMethod = _paymentMethodService.GetPaymentMethodByName("Cash");

            foreach (QueuedSMS qe in queuedsms)
            {
                try
                {                                        
                    if(qe.ContractId.HasValue && qe.Charged.HasValue && !qe.Charged.Value)
                    {
                        SavingBookContract _saving = _savingService.GetSaving(qe.ContractId.Value);

                        //Get all sms for same saving book contract
                        var smsGroup = _queuedSMSService.FindByContractId(_saving.Id);

                        string description = "";

                        string formatedAccountNumber = "******" + _saving.Code.Substring(_saving.Code.Length - 4);

                        OCurrency SMSCharges = smsGroup.Count * smsCharge;

                        if (smsGroup.Count > 0)
                        {
                            string desc = "SMS charges of {0:.00} for {1:dd.MM.yyyy} - {2:dd.MM.yyyy} : {3}";
                            object[] items = new object[] { SMSCharges.GetFormatedValue(true), smsGroup.First().CreatedOnUtc, smsGroup.Last().CreatedOnUtc, formatedAccountNumber };
                            description = string.Format(desc, items);                            
                        }
                        if (smsGroup.Count == 0)
                        {
                            string desc = "SMS charges of {0:.00} for {1:dd.MM.yyyy} : {3}";
                            object[] items = new object[] { SMSCharges.GetFormatedValue(true), smsGroup.First().CreatedOnUtc, formatedAccountNumber };
                            description = string.Format(desc, items);
                            smsGroup.First().Charged = true;
                        }
                        _savingService.Withdraw(_saving, DateTime.Now, SMSCharges, true, description, "", adminUser,
                                Teller.CurrentTeller, paymentMethod);

                        qe.Charged = true;
                        foreach (var sms in smsGroup)
                        {
                            sms.Charged = true;
                            queuedsms.Where(s => s.Id == sms.Id).First().Charged = true;
                            _queuedSMSService.Update(sms);
                        }

                        //Send sms charge notification
                        Person person = _clientService.FindPersonById(qe.RecipientId.Value);
                        if(person != null)
                            if (person.SMSDelivery.HasValue && person.SMSDelivery.Value)
                            {
                                string mfbName = Convert.ToString(ServicesProvider.GetInstance().GetGeneralSettings().GetSpecificParameter(OGeneralSettings.MFI_NAME));
                                //var message = messageTemplate.Body;
                                var messageReplaced = mfbName + " " + description;// Tokenizer.Replace(message, tokens, false);

                                var sms = new QueuedSMS()
                                {
                                    From = Convert.ToString(ServicesProvider.GetInstance().GetGeneralSettings().GetSpecificParameter(OGeneralSettings.SMS_FROM_NUMBER)),
                                    Recipient = person.PersonalPhone,
                                    RecipientId = person.Id,
                                    ContractId = _saving != null ? _saving.Id : 0,
                                    Charged = true,
                                    Message = messageReplaced,
                                    SentTries = 0,
                                    CreatedOnUtc = DateTime.UtcNow,
                                };

                                _queuedSMSService.Add(sms);
                            }
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(string.Format("Error charging sms: {0}", exc.Message), exc);
                }
                finally
                {                    
                   
                }
            }
        }
    }
}
