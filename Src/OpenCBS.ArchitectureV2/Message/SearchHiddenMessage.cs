using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class SearchHiddenMessage : ITinyMessage
    {
        public SearchHiddenMessage(object sender)
        {
            Sender = sender;
        }

        public object Sender { get; private set; }
    }
}
