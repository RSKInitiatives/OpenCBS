using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class StartPageShownMessage : ITinyMessage
    {
        public StartPageShownMessage(object sender)
        {
            Sender = sender;
        }
        public object Sender { get; private set; }
    }
}
