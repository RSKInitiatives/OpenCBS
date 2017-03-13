using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Loan
{
    public class PenaltyWriteOffEvent : Event
    {
        public override string Code
        {
            get { return "PWOE"; }
            set { base.Code = value; }
        }

        public OCurrency Amount { get; set; }
    }
}
