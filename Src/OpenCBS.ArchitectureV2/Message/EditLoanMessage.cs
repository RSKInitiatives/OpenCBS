using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class EditLoanMessage : ITinyMessage
    {
        public EditLoanMessage(object sender, int id)
        {
            Sender = sender;
            Id = id;
        }
        
        public object Sender { get; private set; }
        public int Id { get; private set; }
    }
}
