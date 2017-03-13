using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class DashboardShownMessage : ITinyMessage
    {
        public DashboardShownMessage(object sender)
        {
            Sender = sender;
        }
        
        public object Sender { get; private set; }
    }
}
