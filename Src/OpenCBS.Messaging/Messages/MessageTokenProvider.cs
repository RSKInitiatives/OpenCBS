using OpenCBS.Messaging.Custom;
using OpenCBS.Messaging.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using OpenCBS.Messaging.Interfaces;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Services.Messaging;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Saving;
using System.Text.RegularExpressions;

namespace OpenCBS.Messaging.Messages
{
    public partial class MessageTokenProvider : IMessageTokenProvider
    {
        #region Fields

        [Import]
        public EmailAccountServices EmailAccountService { get; set; }
        [Import]
        public EmailAccountSettings EmailAccountSettings { get; set; }
        [Import]
        public IDateTimeHelper DateTimeHelper { get; set; }

        #endregion

        #region Ctor

        public MessageTokenProvider()
        {
        }

        #endregion

        #region Methods
        public virtual void AddUserTokens(IList<Token> tokens, User user)
        {
            tokens.Add(new Token("Staff.Id", user.Id.ToString()));
        }

        public void AddPersonTokens(IList<Token> tokens, Person person)
        {
            tokens.Add(new Token("Person.Id", person.Id.ToString()));
            tokens.Add(new Token("Person.FullName", person.FullName.ToString()));
            tokens.Add(new Token("Person.FirstName", person.FirstName.ToString()));
            tokens.Add(new Token("Person.LastName", person.LastName.ToString()));
            tokens.Add(new Token("Person.Sex", person.Sex.ToString()));
            tokens.Add(new Token("Person.Nationality", person.Nationality.ToString()));
            //tokens.Add(new Token("Person.Address", person.Address.ToString()));
            tokens.Add(new Token("Person.DateOfBirth", person.DateOfBirth.ToString()));
            //tokens.Add(new Token("Person.FatherName", person.FatherName.ToString()));
            tokens.Add(new Token("Person.HomePhone", person.HomePhone.ToString()));
            tokens.Add(new Token("Person.PersonalPhone", person.PersonalPhone.ToString()));
            tokens.Add(new Token("Person.NbOfloans", person.NbOfloans.ToString()));
            tokens.Add(new Token("Person.NbOfProjects", person.NbOfProjects.ToString()));
            tokens.Add(new Token("Person.Status", person.Status.ToString()));
            tokens.Add(new Token("Person.Type", person.Type.Description()));
        }

        public void AddSavingEventTokens(List<Token> tokens, SavingEvent savingEvent, ISavingsContract savingsContract)
        {
            string description = savingEvent.Description;
            string formatedAccountNumber = "******" + savingsContract.Code.Substring(savingsContract.Code.Length - 4);
            if (description.Contains(savingsContract.Code))
                description.Replace(savingsContract.Code, formatedAccountNumber);            

            tokens.Add(new Token("SavingEvent.Id", savingEvent.Id.ToString()));
            tokens.Add(new Token("SavingEvent.Code", savingEvent.Code));
            tokens.Add(new Token("SavingEvent.Description", description));
            tokens.Add(new Token("SavingEvent.ReferenceNumber", savingEvent.ReferenceNumber));
            tokens.Add(new Token("SavingEvent.PaymentsMethod", savingEvent.PaymentsMethod != null ? savingEvent.PaymentsMethod.Name : ""));
            tokens.Add(new Token("SavingEvent.Date", savingEvent.Date.ToShortDateString()));
            tokens.Add(new Token("SavingEvent.Currency", savingEvent.Currency != null ? savingEvent.Currency.Name : "NAIRA"));
            tokens.Add(new Token("SavingEvent.Amount", savingEvent.Amount.GetFormatedValue(true)));
            tokens.Add(new Token("SavingEvent.Branch", savingEvent.Branch != null ? savingEvent.Branch.Name : ""));
            tokens.Add(new Token("SavingEvent.Comment", savingEvent.Comment));
            tokens.Add(new Token("SavingEvent.IsPending", savingEvent.IsPending.ToString()));
            tokens.Add(new Token("SavingEvent.SavingProduct.Code", savingEvent.SavingProduct != null ? savingEvent.SavingProduct.Code : ""));
            tokens.Add(new Token("SavingEvent.SavingsMethod", savingEvent.SavingsMethod != null ? savingEvent.SavingsMethod.Description() : ""));
            tokens.Add(new Token("SavingEvent.AccountNumber", formatedAccountNumber));
            
            tokens.Add(new Token("SavingEvent.AccountBalance", savingsContract.GetFmtBalance(true)));
            tokens.Add(new Token("SavingEvent.AccountAvailableBalance", savingsContract.GetFmtAvailBalance(true)));
        }

        public static string[] GetListOfAllowedTokens()
        {
            var allowedTokens = new List<string>()
            {
                "[Allowed Tokens]",
                "%Person.Id%",
                "%Person.FullName%",
                "%Person.FirstName%",
                "%Person.LastName%",
                "%Person.Sex%",
                "%Person.Nationality%",
                //"%Person.Address%",
                "%Person.DateOfBirth%",
                "%Person.FatherName%",
                "%Person.HomePhone%",
                "%Person.PersonalPhone%",
                "%Person.NbOfloans%", 
                "%Person.NbOfProjects%",
                "%Person.Status%",
                "%Person.Type%",  
              
                "%Staff.Id%",


                "%SavingEvent.Id%",
                "%SavingEvent.Code%",
                "%SavingEvent.Description%",
                "%SavingEvent.ReferenceNumber%",
                "%SavingEvent.PaymentsMethod%",
                "%SavingEvent.Date%",
                "%SavingEvent.Currency%", 
                "%SavingEvent.Amount%",
                "%SavingEvent.Branch%",
                "%SavingEvent.Comment%",
                "%SavingEvent.IsPending%",
                "%SavingEvent.SavingProduct.Code%",
                "%SavingEvent.SavingsMethod%",
                "%SavingEvent.AccountNumber%",

                "%SavingEvent.AccountBalance%",
                "%SavingEvent.AccountAvailableBalance%",

                "%Messaging.OperationCode%",
                "%Messaging.CurrentDate%",
                "%Messaging.CurrentTime%",
            };
            return allowedTokens.ToArray();
        }

        #endregion

    }
}
