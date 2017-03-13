using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Messaging.Custom;
using OpenCBS.Messaging.Messages;
using System;
using System.Collections.Generic;

namespace OpenCBS.Messaging.Interfaces
{
    public partial interface IMessageTokenProvider
    {
		void AddUserTokens(IList<Token> tokens, User user);

        void AddPersonTokens(IList<Token> tokens, Person person);

        void AddSavingEventTokens(List<Token> tokens, SavingEvent savingsEvent, ISavingsContract savingsContract);
    }
}
