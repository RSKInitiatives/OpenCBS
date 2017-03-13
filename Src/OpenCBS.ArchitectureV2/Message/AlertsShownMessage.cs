using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class AlertsShownMessage : ITinyMessage
    {
        public AlertsShownMessage(object sender)
        {
            Sender = sender;
        }
        public object Sender { get; private set; }
    }
}
