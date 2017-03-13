using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Enums;
using System;
using System.Collections.Generic;

namespace OpenCBS.Messaging.Interfaces
{
    public partial interface IMessagingService
    {        
        int SendNotification(SavingEvent savingEvent, ISavingsContract savingsContract, String savingsOperation);        
    }
}
