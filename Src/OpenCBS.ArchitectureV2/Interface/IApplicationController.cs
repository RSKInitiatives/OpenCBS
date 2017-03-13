using System;
using System.Collections.Generic;
using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Interface
{    
    public interface IApplicationController
    {
        void Execute<T>(T commandData);
        void Subscribe<T>(object receiver, Action<T> action) where T : class, ITinyMessage;
        void Unsubscribe(object reciever);
        void Publish<T>(T message) where T : class, ITinyMessage;
        IList<T> GetAllInstances<T>();
        void ShowError(Exception error);
    }
}
