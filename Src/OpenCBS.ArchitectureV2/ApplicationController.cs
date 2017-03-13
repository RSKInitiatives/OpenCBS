using System;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.View;
using StructureMap;
using TinyMessenger;
using System.ComponentModel.Composition;

namespace OpenCBS.ArchitectureV2
{
    [Export(typeof(IApplicationController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApplicationController : IApplicationController
    {
        private readonly IContainer _container;
        private readonly ITinyMessengerHub _messengerHub;
        private readonly Dictionary<object, List<TinyMessageSubscriptionToken>> _subscriptions = new Dictionary<object, List<TinyMessageSubscriptionToken>>();

        public ApplicationController(IContainer container, ITinyMessengerHub messengerHub)
        {
            _container = container;
            _messengerHub = messengerHub;
        }

        public void Execute<T>(T commandData)
        {
            try
            {
                var command = _container.TryGetInstance<ICommand<T>>();
                if (command != null)
                {
                    command.Execute(commandData);
                }
            }
            catch (Exception error)
            {
                ShowError(error);
            }
        }

        public IList<T> GetAllInstances<T>()
        {
            return _container.GetAllInstances<T>().ToList();
        }

        public void Subscribe<T>(object receiver, Action<T> action) where T : class, ITinyMessage
        {
            if (!_subscriptions.ContainsKey(receiver))
            {
                _subscriptions[receiver] = new List<TinyMessageSubscriptionToken>();
            }
            var token = _messengerHub.Subscribe(action);
            _subscriptions[receiver].Add(token);
        }

        public void Unsubscribe(object receiver)
        {
            if (!_subscriptions.ContainsKey(receiver))
            {
                return;
            }
            foreach (var token in _subscriptions[receiver])
            {
                token.Dispose();
            }
            _subscriptions.Remove(receiver);
        }

        public void Publish<T>(T message) where T : class, ITinyMessage
        {
            _messengerHub.Publish(message);
        }

        public void ShowError(Exception error)
        {
            var errorView = _container.GetInstance<IErrorView>();
            errorView.Run(error.Message);
        }
    }
}
