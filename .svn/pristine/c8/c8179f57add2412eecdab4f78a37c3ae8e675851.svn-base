using TinyMessenger;

namespace OpenCBS.ArchitectureV2.Message
{
    public class FastRepaymentDoneMessage : ITinyMessage
    {
        public FastRepaymentDoneMessage(object sender, int villageBankId)
        {
            Sender = sender;
            VillageBankId = villageBankId;
        }

        public object Sender { get; private set; }

        public int VillageBankId { get; private set; }
    }
}
