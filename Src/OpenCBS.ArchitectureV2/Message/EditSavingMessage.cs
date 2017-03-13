using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class EditSavingMessage : ITinyMessage
    {
        public EditSavingMessage(object sender, int id)
        {
            Sender = sender;
            Id = id;
        }

        public object Sender { get; private set; }

        public int Id { get; private set; }
    }
}
